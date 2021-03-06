﻿using System;
using System.Drawing;
using System.Windows.Forms;

using BizHawk.Client.Common;
using BizHawk.Emulation.Common;

using BizHawk.Emulation.Cores.Nintendo.N64;
using BizHawk.Emulation.Cores.Nintendo.SNES;
using BizHawk.Emulation.Cores.Sega.Saturn;
using BizHawk.Emulation.Cores.Consoles.Sega.gpgx;
using BizHawk.Emulation.Cores.Sega.MasterSystem;
using BizHawk.Emulation.Cores.ColecoVision;
using BizHawk.Emulation.Cores.Atari.Atari2600;

namespace BizHawk.Client.EmuHawk
{
	public partial class ProfileConfig : Form
	{
		public ProfileConfig()
		{
			InitializeComponent();
		}

		private void ProfileConfig_Load(object sender, EventArgs e)
		{
			if (!VersionInfo.DeveloperBuild)
			{
				ProfileSelectComboBox.Items.Remove("Custom Profile");
			}

			switch (Global.Config.SelectedProfile)
			{
				default:
				case Config.ClientProfile.Custom: // For now
				case Config.ClientProfile.Casual:
					ProfileSelectComboBox.SelectedItem = "Casual Gaming";
					break;
				case Config.ClientProfile.Longplay:
					ProfileSelectComboBox.SelectedItem = "Longplays";
					break;
				case Config.ClientProfile.Tas:
					ProfileSelectComboBox.SelectedItem = "Tool-assisted Speedruns";
					break;
			}
		}

		private void OkBtn_Click(object sender, EventArgs e)
		{
			if (ProfileSelectComboBox.SelectedIndex == 0) // Casual Gaming
			{
				DisplayProfileSettingBoxes(false);
				Global.Config.SaveLargeScreenshotWithStates = false;
				Global.Config.SaveScreenshotWithStates = false;
				Global.Config.AllowUD_LR = false;
				Global.Config.BackupSavestates = false;

				Global.Config.RewindEnabledLarge = false;
				Global.Config.RewindEnabledMedium = false;
				Global.Config.RewindEnabledSmall = true;

				// N64
				var n64Settings = GetSyncSettings<N64, N64SyncSettings>();
				n64Settings.Rsp = N64SyncSettings.RspType.Rsp_Hle;
				n64Settings.Core = N64SyncSettings.CoreType.Dynarec;
				Global.Config.N64UseCircularAnalogConstraint = true;
				PutSyncSettings<N64>(n64Settings);

				// SNES
				var snesSettings = GetSyncSettings<LibsnesCore, LibsnesCore.SnesSyncSettings>();
				snesSettings.Profile = "Performance";
				PutSyncSettings<LibsnesCore>(snesSettings);

				//Saturn
				var saturnSettings = GetSyncSettings<Yabause, Yabause.SaturnSyncSettings>();
				saturnSettings.SkipBios = false;
				PutSyncSettings<Yabause>(saturnSettings);

				//Genesis
				var genesisSettings = GetSyncSettings<GPGX, GPGX.GPGXSyncSettings>();
				genesisSettings.Region = LibGPGX.Region.Autodetect;
				PutSyncSettings<GPGX>(genesisSettings);

				//SMS
				var smsSettings = GetSyncSettings<SMS, SMS.SMSSyncSettings>();
				smsSettings.UseBIOS = false;
				PutSyncSettings<SMS>(smsSettings);

				//Coleco
				var colecoSettings = GetSyncSettings<ColecoVision, ColecoVision.ColecoSyncSettings>();
				colecoSettings.SkipBiosIntro = false;
				PutSyncSettings<ColecoVision>(colecoSettings);

				//A2600
				var a2600Settings = GetSyncSettings<Atari2600, Atari2600.A2600SyncSettings>();
				a2600Settings.FastScBios = true;
				a2600Settings.LeftDifficulty = false;
				a2600Settings.RightDifficulty = false;
				PutSyncSettings<Atari2600>(a2600Settings);

				// NES
				Global.Config.NES_InQuickNES = true;
			}
			else if (ProfileSelectComboBox.SelectedIndex == 2) // Long Plays
			{
				DisplayProfileSettingBoxes(false);
				Global.Config.SaveLargeScreenshotWithStates = false;
				Global.Config.SaveScreenshotWithStates = false;
				Global.Config.AllowUD_LR = false;
				Global.Config.BackupSavestates = false;

				Global.Config.RewindEnabledLarge = false;
				Global.Config.RewindEnabledMedium = false;
				Global.Config.RewindEnabledSmall = true;

				// N64
				var n64Settings = GetSyncSettings<N64, N64SyncSettings>();
				n64Settings.Rsp = N64SyncSettings.RspType.Rsp_Z64_hlevideo;
				n64Settings.Core = N64SyncSettings.CoreType.Pure_Interpret;
				Global.Config.N64UseCircularAnalogConstraint = true;
				PutSyncSettings<N64>(n64Settings);

				// SNES
				var snesSettings = GetSyncSettings<LibsnesCore, LibsnesCore.SnesSyncSettings>();
				snesSettings.Profile = "Compatibility";
				PutSyncSettings<LibsnesCore>(snesSettings);

				// Saturn
				var saturnSettings = GetSyncSettings<Yabause, Yabause.SaturnSyncSettings>();
				saturnSettings.SkipBios = false;
				PutSyncSettings<Yabause>(saturnSettings);

				// Genesis
				var genesisSettings = GetSyncSettings<GPGX, GPGX.GPGXSyncSettings>();
				genesisSettings.Region = LibGPGX.Region.Autodetect;
				PutSyncSettings<GPGX>(genesisSettings);

				// SMS
				var smsSettings = GetSyncSettings<SMS, SMS.SMSSyncSettings>();
				smsSettings.UseBIOS = false;
				PutSyncSettings<SMS>(smsSettings);

				// Coleco
				var colecoSettings = GetSyncSettings<ColecoVision, ColecoVision.ColecoSyncSettings>();
				colecoSettings.SkipBiosIntro = false;
				PutSyncSettings<ColecoVision>(colecoSettings);

				// A2600
				var a2600Settings = GetSyncSettings<Atari2600, Atari2600.A2600SyncSettings>();
				a2600Settings.FastScBios = false;
				a2600Settings.LeftDifficulty = true;
				a2600Settings.RightDifficulty = true;
				PutSyncSettings<Atari2600>(a2600Settings);

				// NES
				Global.Config.NES_InQuickNES = true;
			}
			else if (ProfileSelectComboBox.SelectedIndex == 1) // TAS
			{
				DisplayProfileSettingBoxes(false);

				// General
				Global.Config.SaveLargeScreenshotWithStates = true;
				Global.Config.SaveScreenshotWithStates = true;
				Global.Config.AllowUD_LR = true;
				Global.Config.BackupSavestates = true;

				// Rewind
				Global.Config.RewindEnabledLarge = false;
				Global.Config.RewindEnabledMedium = false;
				Global.Config.RewindEnabledSmall = false;

				// N64
				var n64Settings = GetSyncSettings<N64, N64SyncSettings>();
				n64Settings.Rsp = N64SyncSettings.RspType.Rsp_Z64_hlevideo;
				n64Settings.Core = N64SyncSettings.CoreType.Pure_Interpret;
				Global.Config.N64UseCircularAnalogConstraint = false;
				PutSyncSettings<N64>(n64Settings);

				// SNES
				var snesSettings = GetSyncSettings<LibsnesCore, LibsnesCore.SnesSyncSettings>();
				snesSettings.Profile = "Compatibility";
				PutSyncSettings<LibsnesCore>(snesSettings);

				// Saturn
				var saturnSettings = GetSyncSettings<Yabause, Yabause.SaturnSyncSettings>();
				saturnSettings.SkipBios = true;
				PutSyncSettings<Yabause>(saturnSettings);

				// Genesis
				var genesisSettings = GetSyncSettings<GPGX, GPGX.GPGXSyncSettings>();
				genesisSettings.Region = LibGPGX.Region.Autodetect;
				PutSyncSettings<GPGX>(genesisSettings);

				// SMS
				var smsSettings = GetSyncSettings<SMS, SMS.SMSSyncSettings>();
				smsSettings.UseBIOS = false;
				PutSyncSettings<SMS>(smsSettings);

				// Coleco
				var colecoSettings = GetSyncSettings<ColecoVision, ColecoVision.ColecoSyncSettings>();
				colecoSettings.SkipBiosIntro = true;
				PutSyncSettings<ColecoVision>(colecoSettings);

				// A2600
				var a2600Settings = GetSyncSettings<Atari2600, Atari2600.A2600SyncSettings>();
				a2600Settings.FastScBios = false;
				a2600Settings.LeftDifficulty = true;
				a2600Settings.RightDifficulty = true;
				PutSyncSettings<Atari2600>(a2600Settings);

				// NES
				Global.Config.NES_InQuickNES = true;
			}
			else if (ProfileSelectComboBox.SelectedIndex == 3) //custom
			{
				//Disabled for now
				//DisplayProfileSettingBoxes(true);
			}

			switch(ProfileSelectComboBox.SelectedItem.ToString())
			{
				default:
				case "Custom Profile": // For now
				case "Casual Gaming":
					Global.Config.SelectedProfile = Config.ClientProfile.Casual;
					break;
				case "Longplays":
					Global.Config.SelectedProfile = Config.ClientProfile.Longplay;
					break;
				case "Tool-assisted Speedruns":
					Global.Config.SelectedProfile = Config.ClientProfile.Tas;
					break;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void DisplayProfileSettingBoxes(bool cProfile)
		{
			if (cProfile)
			{
				ProfileDialogHelpTexBox.Location = new Point(217, 12);
				ProfileDialogHelpTexBox.Size = new Size(165, 126);
				SaveScreenshotStatesCheckBox.Visible = true;
				SaveLargeScreenshotStatesCheckBox.Visible = true;
				AllowUDLRCheckBox.Visible = true;
				GeneralOptionsLabel.Visible = true;
			}
			else
			{
				ProfileDialogHelpTexBox.Location = new Point(184, 12);
				ProfileDialogHelpTexBox.Size = new Size(198, 126);
				ProfileDialogHelpTexBox.Text = "Options: \r\nCasual Gaming - All about performance! \r\n\nTool-Assisted Speedruns - Maximum Accuracy! \r\n\nLongplays - Stability is the key!";
				SaveScreenshotStatesCheckBox.Visible = false;
				SaveLargeScreenshotStatesCheckBox.Visible = false;
				AllowUDLRCheckBox.Visible = false;
				GeneralOptionsLabel.Visible = false;
			}
		}

		private void SaveScreenshotStatesCheckBox_MouseHover(object sender, EventArgs e)
		{
			ProfileDialogHelpTexBox.Text = "Save Screenshot with Savestates: \r\n * Required for TASing \r\n * Not Recommended for \r\n   Longplays or Casual Gaming";
		}
		private void SaveLargeScreenshotStatesCheckBox_MouseHover(object sender, EventArgs e)
		{
			ProfileDialogHelpTexBox.Text = "Save Large Screenshot With States: \r\n * Required for TASing \r\n * Not Recommended for \r\n   Longplays or Casual Gaming";
		}
		private void AllowUDLRCheckBox_MouseHover(object sender, EventArgs e)
		{
			ProfileDialogHelpTexBox.Text = "All Up+Down or Left+Right: \r\n * Useful for TASing \r\n * Unchecked for Casual Gaming \r\n * Unknown for longplays";
		}

		private static TSetting GetSyncSettings<TEmulator, TSetting>()
			where TSetting : class, new()
			where TEmulator : IEmulator
		{
			// should we complain if we get a successful object from the config file, but it is the wrong type?
			return Global.Emulator.GetSyncSettings() as TSetting
				?? Global.Config.GetCoreSyncSettings<TEmulator>() as TSetting
				?? new TSetting(); // guaranteed to give sensible defaults
		}

		private static TSetting GetSettings<TEmulator, TSetting>()
			where TSetting : class, new()
			where TEmulator : IEmulator
		{
			// should we complain if we get a successful object from the config file, but it is the wrong type?
			return Global.Emulator.GetSettings() as TSetting
				?? Global.Config.GetCoreSettings<TEmulator>() as TSetting
				?? new TSetting(); // guaranteed to give sensible defaults
		}

		private static void PutSettings<TEmulator>(object o)
			where TEmulator : IEmulator
		{
			if (Global.Emulator is TEmulator)
			{
				Global.Emulator.PutSettings(o);
			}
			else
			{
				Global.Config.PutCoreSettings<TEmulator>(o);
			}
		}

		private static void PutSyncSettings<TEmulator>(object o)
			where TEmulator : IEmulator
		{
			if (Global.Emulator is TEmulator)
			{
				GlobalWin.MainForm.PutCoreSyncSettings(o);
			}
			else
			{
				Global.Config.PutCoreSyncSettings<TEmulator>(o);
			}
		}
	}
}
