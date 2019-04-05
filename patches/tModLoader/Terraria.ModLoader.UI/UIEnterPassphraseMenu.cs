﻿using System;
using System.Diagnostics;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	internal class UIEnterPassphraseMenu : UIState
	{
		public UITextPanel<string> uITextPanel;
		internal UIInputTextField passcodeTextField;
		private readonly string registerURL = "http://javid.ddns.net/tModLoader/register.php";
		private int gotoMenu;

		public override void OnInitialize() {
			var uIElement = new UIElement {
				Width = { Percent = 0.8f },
				MaxWidth = UICommon.MAX_PANEL_WIDTH,
				Top = { Pixels = 220 },
				Height = { Pixels = -220, Percent = 1f },
				HAlign = 0.5f
			};

			var uIPanel = new UIPanel {
				Width = { Percent = 1f },
				Height = { Pixels = -110, Percent = 1f },
				BackgroundColor = UICommon.MAIN_PANEL_BG_COLOR,
				PaddingTop = 0f
			};
			uIElement.Append(uIPanel);

			uITextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.MBPublishEnterPassphrase"), 0.8f, true) {
				HAlign = 0.5f,
				Top = { Pixels = -35 },
				BackgroundColor = UICommon.UI_BLUE_COLOR
			}.WithPadding(15);
			uIElement.Append(uITextPanel);

			var buttonBack = new UITextPanel<string>(Language.GetTextValue("UI.Back")) {
				Width = { Pixels = -10, Percent = 0.5f },
				Height = { Pixels = 25 },
				VAlign = 1f,
				Top = { Pixels = -65 }
			}.WithFadedMouseOver();
			buttonBack.OnClick += BackClick;
			uIElement.Append(buttonBack);

			var buttonSubmit = new UITextPanel<string>(Language.GetTextValue("UI.Submit"));
			buttonSubmit.CopyStyle(buttonBack);
			buttonSubmit.HAlign = 1f;
			buttonSubmit.WithFadedMouseOver();
			buttonSubmit.OnClick += OKClick;
			uIElement.Append(buttonSubmit);

			var buttonVisitWebsite = new UITextPanel<string>(Language.GetTextValue("tModLoader.MBPublishVisitWebsiteForPassphrase"));
			buttonVisitWebsite.CopyStyle(buttonBack);
			buttonVisitWebsite.Width.Percent = 1f;
			buttonVisitWebsite.Top.Pixels = -20;
			buttonVisitWebsite.WithFadedMouseOver();
			buttonVisitWebsite.OnClick += VisitRegisterWebpage;
			uIElement.Append(buttonVisitWebsite);

			passcodeTextField = new UIInputTextField(Language.GetTextValue("tModLoader.MBPublishPastePassphrase")) {
				HAlign = 0.5f,
				VAlign = 0.5f,
				Left = { Pixels = -100, Percent = 0 }
			};
			passcodeTextField.OnTextChange += OnTextChange;
			uIPanel.Append(passcodeTextField);

			Append(uIElement);
		}

		internal void SetGotoMenu(int gotoMenu) {
			this.gotoMenu = gotoMenu;
		}

		private void BackClick(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(11, -1, -1, 1);
			Main.menuMode = gotoMenu;
		}

		private void OKClick(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(10, -1, -1, 1);
			ModLoader.modBrowserPassphrase = passcodeTextField.Text.Trim();
			Main.SaveSettings();
#if GOG
			Main.menuMode = Interface.enterSteamIDMenuID;
			Interface.enterSteamIDMenu.SetGotoMenu(this.gotoMenu);
#else
			Main.menuMode = gotoMenu;
#endif
		}

		private void OnTextChange(object sender, EventArgs e) { }

		private void VisitRegisterWebpage(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(10, -1, -1, 1);
			Process.Start(registerURL);
		}
	}
}
