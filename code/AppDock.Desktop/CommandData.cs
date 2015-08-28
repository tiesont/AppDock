using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace AppDock.Desktop
{
    public class CommandData
    {
        public string Label { get; set; }

        public string Target { get; set; }

        public string StartInPath { get; set; }

        public ApplicationType Type { get; set; }


        private ICommand _applicationCommand;


        /// <summary>
        /// Gets the execute command.
        /// </summary>
        public ICommand ExecuteCommand
        {
            get
            {
                if (_applicationCommand == null)
                {
                    _applicationCommand = new ApplicationCommand(
                        param => this.Execute(),
                        param => this.CanExecute()
                    );
                }
                return _applicationCommand;
            }

        }


        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecute()
        {
            return this.Type == ApplicationType.Executable ? File.Exists(this.Target) : true;
        }


        /// <summary>
        /// Executes this instance.
        /// </summary>
        private void Execute()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.Target))
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.WorkingDirectory = this.StartInPath;
                        info.FileName = this.Target;
                        Process.Start(info);
                    });
                }
                else
                {
                    MessageBox.Show("The path for this command is either invalid or not currently accessible.", "Command Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ; // TODO: do something with exception
            }
        }
    }
}
