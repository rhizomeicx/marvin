using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marvin
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Swallow the exception, but still log it
        /// </summary>
        /// <param name="action"></param>
        /// <param name="_logger"></param>
        public static void TryNoException(Action action, Logger _logger) {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                _logger.Information(ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
            }    
        }

    }
}
