using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

public static class PleaseWaitRunner
{
	private static Form _waitForm;
	private static Label _waitLabel;
	private static Thread _waitThread;

	public static void Run(Action work, Form parentForm, string message = "Processing data...")
	{
		// Start the thread that will display the "please wait" form
		_waitThread = new Thread(() =>
		{
			_waitForm = CreateWaitForm(message);
			Application.Run(_waitForm);
		});
		_waitThread.IsBackground = true;
		_waitThread.SetApartmentState(ApartmentState.STA);
		_waitThread.Start();

		try
		{
			// Execute the long-running task on a separate thread
			var taskThread = new Thread(() =>
			{
				try
				{
					work();
				}
				finally
				{
					CloseWaitForm();
				}
			});
			taskThread.IsBackground = true;
			taskThread.Start();
			taskThread.Join(); // Wait until the task is complete
		}
		catch (Exception ex)
		{
			MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			CloseWaitForm();
		}
	}

	private static Form CreateWaitForm(string initialMessage)
	{
		var waitForm = new Form
		{
			Size = new Size(250, 100),
			Text = "Please Wait",
			FormBorderStyle = FormBorderStyle.FixedDialog,
			StartPosition = FormStartPosition.CenterScreen,
			ControlBox = false
		};

		_waitLabel = new Label
		{
			Text = initialMessage,
			AutoSize = true,
			Location = new Point(50, 30)
		};

		waitForm.Controls.Add(_waitLabel);
		return waitForm;
	}

	private static void CloseWaitForm()
	{
		if (_waitForm != null && _waitForm.InvokeRequired)
		{
			_waitForm.Invoke((MethodInvoker)delegate { _waitForm.Close(); });
		}
		else
		{
			_waitForm?.Close();
		}
	}

	public static void UpdateMessage(string message)
	{
		if (_waitLabel != null && _waitLabel.InvokeRequired)
		{
			_waitLabel.Invoke((MethodInvoker)delegate { _waitLabel.Text = message; });
		}
		else
		{
			_waitLabel.Text = message;
		}
	}
}
