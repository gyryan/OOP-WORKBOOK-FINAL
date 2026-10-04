Imports System.Drawing

Public Class frmDialogsPractice
    Inherits Form

    Private ReadOnly colorDialog As New ColorDialog()
    Private ReadOnly outputLabel As New Label With {.Text = "No color selected.", .Location = New Point(24, 115), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Windows Forms Dialogs Practice"
        ClientSize = New Size(400, 190)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)

        Dim chooseColorButton As New Button With {.Text = "Choose Color", .Location = New Point(24, 35), .Width = 130, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler chooseColorButton.Click, AddressOf ChooseColor
        Controls.Add(chooseColorButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub ChooseColor(sender As Object, e As EventArgs)
        If colorDialog.ShowDialog(Me) = DialogResult.OK Then
            Dim selectedColor As Color = colorDialog.Color
            outputLabel.Text = $"Selected color: {selectedColor.Name} (R={selectedColor.R}, G={selectedColor.G}, B={selectedColor.B})"
            outputLabel.BackColor = selectedColor
            outputLabel.ForeColor = If(selectedColor.GetBrightness() < 0.5, Color.White, Color.Black)
        End If
    End Sub
End Class
