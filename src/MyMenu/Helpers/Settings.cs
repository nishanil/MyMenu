// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace MyMenu.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		static ISettings AppSettings {
			get {
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		const string AccessTokenKey = "settings_key";
		static readonly string AccessTokenDefault = string.Empty;

		const string IsProfileDownloadedKey = "profile_key";
		static readonly bool IsProfileDownloadedDefault = false;

		#endregion


		public static string AccessToken {
			get {
				return AppSettings.GetValueOrDefault (AccessTokenKey, AccessTokenDefault);
			}
			set {
				AppSettings.AddOrUpdateValue (AccessTokenKey, value);
			}
		}

		public static bool IsProfileDownloaded {
			get {
				return AppSettings.GetValueOrDefault<bool> (IsProfileDownloadedKey, IsProfileDownloadedDefault);
			}
			set {
				AppSettings.AddOrUpdateValue<bool> (IsProfileDownloadedKey, value);
			}
		}

	}
}