using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ScriptSDK.Data
{
    /// <summary>
    /// Control extension offering support for graphica interfaces with backgroundworker as script thread.
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Checks if control requires invokation of method or not. Then performs method.
        /// Be aware, there are more and better ways to handle data threadsafe, but this is an easy and 99% working way.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        /// <example>
        /// Example how to assign data to control, and let extension handle the thread safety.
        /// <code language="CSharp">
        /// <![CDATA[
        ///     textbox.SuperInvoke(()=> Text = "Hello World");
        /// ]]>
        /// </code>
        /// </example>
        public static void SuperInvoke(this ISynchronizeInvoke control, MethodInvoker action)
        {
            if (control != null && control.InvokeRequired)
            {
                var args = new object[0];
                control.Invoke(action, args);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Checks if control requires invokation of function or not. Then performs function.
        /// Be aware, there are more and better ways to handle data threadsafe, but this is an easy and 99% working way.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <example>
        /// Example how to read data from control, and let extension handle the thread safety.
        /// <code language="CSharp">
        /// <![CDATA[
        ///   string text = textbox.SuperInvoke(()=> Text);
        /// ]]>
        /// </code>
        /// </example>
        public static T SuperInvoke<T>(this Control control, Func<T> func)
        {
            if (control != null && control.InvokeRequired)
                return (T) (control.Invoke(func));
            return func();
        }
    }
}