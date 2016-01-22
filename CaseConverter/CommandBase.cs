using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CaseConverter
{
    /// <summary>
    /// 拡張機能として登録するコマンドの基底クラスです。
    /// </summary>
    internal abstract class CommandBase
    {
        /// <summary>
        /// コマンドを提供するパッケージです。
        /// </summary>
        private readonly Package _package;

        /// <summary>
        /// シングルトンのインスタンスを取得します。
        /// </summary>
        public static ConvertCaseCommand Instance { get; protected set; }

        /// <summary>
        /// サービスプロバイダーを取得します。
        /// </summary>
        protected IServiceProvider ServiceProvider
        {
            get { return _package; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <remarks>
        /// コマンドは .vsct ファイルに定義されている必要があります。
        /// </remarks>
        /// <param name="package">コマンドを提供するパッケージ</param>
        /// <param name="commandId">コマンドのID</param>
        /// <param name="commandSetId">コマンドメニューグループのID</param>
        protected CommandBase(Package package, int commandId, Guid commandSetId)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            _package = package;

            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(commandSetId, commandId);
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// コマンドを実行した際のコールバックです。
        /// </summary>
        /// <param name="sender">イベントの発行者</param>
        /// <param name="e">イベント引数</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            ShowMessage();
            Execute();
        }

        /// <summary>
        /// メッセージを表示します。
        /// </summary>
        private void ShowMessage()
        {
            var message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", GetType().FullName);
            var title = "Execute Command";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
