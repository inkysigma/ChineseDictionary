#region License
// The PostgreSQL License
//
// Copyright (C) 2015 The Npgsql Development Team
//
// Permission to use, copy, modify, and distribute this software and its
// documentation for any purpose, without fee, and without a written
// agreement is hereby granted, provided that the above copyright notice
// and this paragraph and the following two paragraphs appear in all copies.
//
// IN NO EVENT SHALL THE NPGSQL DEVELOPMENT TEAM BE LIABLE TO ANY PARTY
// FOR DIRECT, INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES,
// INCLUDING LOST PROFITS, ARISING OUT OF THE USE OF THIS SOFTWARE AND ITS
// DOCUMENTATION, EVEN IF THE NPGSQL DEVELOPMENT TEAM HAS BEEN ADVISED OF
// THE POSSIBILITY OF SUCH DAMAGE.
//
// THE NPGSQL DEVELOPMENT TEAM SPECIFICALLY DISCLAIMS ANY WARRANTIES,
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE. THE SOFTWARE PROVIDED HEREUNDER IS
// ON AN "AS IS" BASIS, AND THE NPGSQL DEVELOPMENT TEAM HAS NO OBLIGATIONS
// TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncRewriter;

namespace Npgsql
{
    // ReSharper disable once InconsistentNaming
    internal static class PGUtil
    {
        internal static readonly UTF8Encoding UTF8Encoding = new UTF8Encoding(false, true);
        internal static readonly UTF8Encoding RelaxedUTF8Encoding = new UTF8Encoding(false, false);

        /// <summary>
        /// This method takes a version string as returned by SELECT VERSION() and returns
        /// a valid version string ("7.2.2" for example).
        /// This is only needed when running protocol version 2.
        /// This does not do any validity checks.
        /// </summary>
        public static string ExtractServerVersion(string VersionString)
        {
            Int32 Start = 0, End = 0;

            // find the first digit and assume this is the start of the version number
            for (; Start < VersionString.Length && !Char.IsDigit(VersionString[Start]); Start++)
            {
                ;
            }

            End = Start;

            // read until hitting whitespace, which should terminate the version number
            for (; End < VersionString.Length && !Char.IsWhiteSpace(VersionString[End]); End++)
            {
                ;
            }

            // Deal with this here so that if there are
            // changes in a future backend version, we can handle it here in the
            // protocol handler and leave everybody else put of it.

            VersionString = VersionString.Substring(Start, End - Start + 1);

            for (int idx = 0; idx != VersionString.Length; ++idx)
            {
                char c = VersionString[idx];
                if (!Char.IsDigit(c) && c != '.')
                {
                    VersionString = VersionString.Substring(0, idx);
                    break;
                }
            }

            return VersionString;
        }

        public static int RotateShift(int val, int shift)
        {
            return (val << shift) | (val >> (sizeof (int) - shift));
        }

        /// <summary>
        /// Creates a Task&lt;TResult&gt; that's completed successfully with the specified result.
        /// </summary>
        /// <remarks>
        /// In .NET 4.5 Task provides this. In .NET 4.0 with BCL.Async, TaskEx provides this. This
        /// method wraps the two.
        /// </remarks>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="result">The result to store into the completed task.</param>
        /// <returns>The successfully completed task.</returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static Task<TResult> TaskFromResult<TResult>(TResult result)
        {
#if !NET40
            return Task.FromResult(result);
#else
            return TaskEx.FromResult(result);
#endif
        }

        internal static Task CompletedTask = TaskFromResult(0);

        internal static StringComparer InvariantCaseIgnoringStringComparer
        {
            get
            {
                // Note: not assuming string comparers are threadsafe, although they probably are...
#if DNXCORE50
                return CultureInfo.InvariantCulture.CompareInfo.GetStringComparer(CompareOptions.IgnoreCase);
#else
                return StringComparer.InvariantCultureIgnoreCase;
#endif
            }
        }

        /// <summary>
        /// Throws an exception with the given string and also invokes a contract failure, allowing the static checker
        /// to detect scenarios leading up to this error.
        ///
        /// See http://blogs.msdn.com/b/francesco/archive/2014/09/12/how-to-use-cccheck-to-prove-no-case-is-forgotten.aspx
        /// </summary>
        /// <param name="message">the exception message</param>
        /// <returns>an exception to be thrown</returns>
        [ContractVerification(false)]
        public static Exception ThrowIfReached(string message = null)
        {
            Contract.Requires(false);
            return message == null ? new Exception("An internal Npgsql occured, please open an issue in http://github.com/npgsql/npgsql with this exception's stack trace") : new Exception(message);
        }
    }

    /// <summary>
    /// Represent the frontend/backend protocol version.
    /// </summary>
    public enum ProtocolVersion
    {
        /// <summary>
        /// Protocol version 3 (the current version).
        /// </summary>
        Version3 = 3
    }

    internal enum FormatCode : short
    {
        Text = 0,
        Binary = 1
    }

    internal static class EnumerableExtensions
    {
        internal static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }
    }

    /// <summary>
    /// Represents a timeout that will expire at some point.
    /// </summary>
    internal struct NpgsqlTimeout
    {
        readonly DateTime _expiration;
        internal DateTime Expiration { get { return _expiration; } }

        internal static NpgsqlTimeout Infinite = new NpgsqlTimeout(TimeSpan.Zero);

        internal NpgsqlTimeout(TimeSpan expiration)
        {
            _expiration = expiration == TimeSpan.Zero
                ? DateTime.MaxValue
                : DateTime.Now + expiration;
        }

        internal void Check()
        {
            if (HasExpired)
                throw new TimeoutException();
        }

        internal bool IsSet
        {
            get { return _expiration != DateTime.MaxValue; }
        }

        internal bool HasExpired
        {
            get { return DateTime.Now >= Expiration; }
        }

        internal Task AsTask
        {
            get { return Task.Delay(TimeLeft); }
        }

        internal TimeSpan TimeLeft { get { return Expiration - DateTime.Now; } }
    }
}
