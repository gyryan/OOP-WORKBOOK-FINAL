Public Class frmMdiPractice
    Inherits Form

    Private ReadOnly documentInput As New TextBox With {.Location = New Point(24, 48), .Width = 270}

    Public Sub New()
        Text = "MDI Practice"
        ClientSize = New Size(570, 360)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        IsMdiContainer = True
        Controls.Add(New Label With {.Text = "Text for a new child window:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(documentInput)

        Dim openButton As New Button With {.Text = "Open Child Window", .Location = New Point(310, 46), .Width = 150, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler openButton.Click, AddressOf OpenChildWindow
        Controls.Add(openButton)
    End Sub

    Private Sub OpenChildWindow(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(documentInput.Text) Then
            MessageBox.Show(Me, "Enter text for the child window.", "MDI Practice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim childWindow As New Form With {
            .Text = "Document " & (MdiChildren.Length + 1).ToString(),
            .MdiParent = Me,
            .BackColor = Color.FromArgb(18, 40, 72),
            .Size = New Size(280, 150)
        }
        childWindow.Controls.Add(New Label With {
            .Text = documentInput.Text,
            .AutoSize = True,
            .Location = New Point(20, 20),
            .ForeColor = Color.White
        })
        childWindow.Show()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'frmMdiPractice
        '
        Me.ClientSize = New System.Drawing.Size(1451, 253)
        Me.Name = "frmMdiPractice"
        Me.ResumeLayout(False)

    End Sub
End Class
