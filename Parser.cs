using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Media;
using System.Drawing;

namespace DoThat
{
	class Parser
	{
		public static void Main(string[] Arguments)
		{
			if (File.Exists("Script.ds") == true) {
				try {
					foreach (string Line in File.ReadAllLines("Script.ds")) {
						if (Line.ToString().ToUpperInvariant().Contains("PLAYSOUND") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("PLAYSOUND ", string.Empty);
							
							try {
								switch (ParseScript.ToString().ToUpperInvariant()) {
									case "ASTERISK":
										SystemSounds.Asterisk.Play();
										break;
										
									case "BEEP":
										SystemSounds.Beep.Play();
										break;
										
									case "EXCLAMATION":
										SystemSounds.Exclamation.Play();
										break;
										
									case "HAND":
										SystemSounds.Hand.Play();
										break;
										
									case "QUESTION":
										SystemSounds.Question.Play();
										break;
								}
							} catch (Exception FailedToParse) {
								var ParseError = "[ERROR]\n\n" + FailedToParse.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
						
						if (Line.ToString().ToUpperInvariant().Contains("WAIT") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("WAIT ", string.Empty);
							
							int WaitTime = 0;
							
							try {
								if (int.TryParse(ParseScript.ToString(), out WaitTime) == true) {
									Thread.Sleep(WaitTime);
								}
							} catch (Exception FailedToWait) {
								var ParseError = "[ERROR]\n\n" + FailedToWait.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
						
						if (Line.ToString().ToUpperInvariant().Contains("START") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("START ", string.Empty);
							
							try {
								Process.Start(ParseScript.ToString());
							} catch (Exception FailedToExecute) {
								var ParseError = "[ERROR]\n\n" + FailedToExecute.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
						
						if (Line.ToString().ToUpperInvariant().Contains("MESSAGE") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("MESSAGE ", string.Empty);
							
							try {
								MessageBox.Show(ParseScript.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
							} catch (Exception FailedToCreateMessage) {
								var ParseError = "[ERROR]\n\n" + FailedToCreateMessage.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
						
						if (Line.ToString().ToUpperInvariant().Contains("SENDKEYS") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("SENDKEYS ", string.Empty);
							
							try {
								SendKeys.SendWait(ParseScript.ToString());
							} catch (Exception FailedToSendKeys) {
								var ParseError = "[ERROR]\n\n" + FailedToSendKeys.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
						
						if (Line.ToString().ToUpperInvariant().Contains("BROWSER") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("BROWSER ", string.Empty);
							
							try {
								Process.Start("https://www.google.com/search?q=" + ParseScript.ToString());
							} catch (Exception FailedToSpawnBrowser) {
								var ParseError = "[ERROR]\n\n" + FailedToSpawnBrowser.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
						
						if (Line.ToString().ToUpperInvariant().Contains("SHELL") == true) {
							var ParseScript = Line.ToString().ToUpperInvariant().Replace("SHELL ", string.Empty);
							
							ProcessStartInfo SpawnShell = new ProcessStartInfo() {
								WindowStyle = ProcessWindowStyle.Hidden,
								CreateNoWindow = true,
								Arguments = "/C " + ParseScript.ToString(),
								UseShellExecute = false,
								FileName = "cmd.exe",
								WorkingDirectory = @"C:\Windows\System32"
							};
							
							try {
								Process.Start(SpawnShell);
							} catch (Exception FailedToSpawnShell) {
								var ParseError = "[ERROR]\n\n" + FailedToSpawnShell.ToString();
								
								MessageBox.Show(ParseError.ToString(), "Script Action Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								
								GC.Collect();
								GC.WaitForFullGCComplete();
								Environment.Exit(1);
							}
						}
					}
				} catch (Exception FailedToReadFile) {
					var ParseError = "[ERROR]\n\n" + FailedToReadFile.ToString();
					
					MessageBox.Show(ParseError.ToString(), "Script Read Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					
					GC.Collect();
					GC.WaitForFullGCComplete();
					Environment.Exit(1);
				}
			} else {
				MessageBox.Show("No script file was found. Make sure a script file exists and try again.\n\nScripts must be named 'Script.ds' and have proper script programming. If the script cannot be read, the program may crash.", "No Script Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				
				GC.Collect();
				GC.WaitForFullGCComplete();
				Environment.Exit(1);
			}
		}
	}
}