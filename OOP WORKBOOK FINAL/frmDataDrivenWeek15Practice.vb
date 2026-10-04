Public Class frmDataDrivenWeek15Practice
    Inherits Form

    Private ReadOnly recordIdInput As New TextBox With {.Location = New Point(24, 48), .Width = 100}
    Private ReadOnly nameInput As New TextBox With {.Location = New Point(150, 48), .Width = 220}
    Private ReadOnly records As New DataTable("Records")
    Private ReadOnly recordGrid As New DataGridView With {.Location = New Point(24, 145), .Size = New Size(500, 220), .ReadOnly = True, .AllowUserToAddRows = False, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
    Private ReadOnly statusLabel As New Label With {.Location = New Point(24, 115), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Data-Driven Applications Week 15 Practice"
        ClientSize = New Size(560, 400)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)

        Dim idColumn As DataColumn = records.Columns.Add("ID", GetType(Integer))
        idColumn.AutoIncrement = True
        idColumn.AutoIncrementSeed = 1
        idColumn.AutoIncrementStep = 1
        records.Columns.Add("Name", GetType(String))
        records.PrimaryKey = New DataColumn() {idColumn}
        recordGrid.DataSource = records

        Controls.Add(New Label With {.Text = "Record ID:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(recordIdInput)
        Controls.Add(New Label With {.Text = "Name:", .Location = New Point(150, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(nameInput)

        Dim addButton As New Button With {.Text = "Add", .Location = New Point(390, 46), .Width = 65, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler addButton.Click, AddressOf AddRecord
        Controls.Add(addButton)
        Dim updateButton As New Button With {.Text = "Update", .Location = New Point(24, 78), .Width = 75, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler updateButton.Click, AddressOf UpdateRecord
        Controls.Add(updateButton)
        Dim deleteButton As New Button With {.Text = "Delete", .Location = New Point(110, 78), .Width = 75, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler deleteButton.Click, AddressOf DeleteRecord
        Controls.Add(deleteButton)
        Dim previousButton As New Button With {.Text = "Previous", .Location = New Point(200, 78), .Width = 85, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler previousButton.Click, Sub(sender, e) MoveRecord(-1)
        Controls.Add(previousButton)
        Dim nextButton As New Button With {.Text = "Next", .Location = New Point(295, 78), .Width = 70, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler nextButton.Click, Sub(sender, e) MoveRecord(1)
        Controls.Add(nextButton)
        Controls.Add(statusLabel)
        Controls.Add(recordGrid)
    End Sub

    Private Sub AddRecord(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(nameInput.Text) Then
            statusLabel.Text = "Enter a name to add a record."
            Return
        End If

        Dim row As DataRow = records.NewRow()
        row("Name") = nameInput.Text.Trim()
        records.Rows.Add(row)
        recordGrid.CurrentCell = recordGrid.Rows(recordGrid.Rows.Count - 1).Cells(0)
        recordIdInput.Text = row("ID").ToString()
        statusLabel.Text = $"Added record with generated ID {row("ID")}."
        nameInput.Clear()
    End Sub

    Private Sub UpdateRecord(sender As Object, e As EventArgs)
        Dim recordId As Integer
        If Not Integer.TryParse(recordIdInput.Text, recordId) OrElse String.IsNullOrWhiteSpace(nameInput.Text) Then
            statusLabel.Text = "Enter a valid record ID and name to update."
            Return
        End If

        Dim row As DataRow = records.Rows.Find(recordId)
        If row Is Nothing Then
            statusLabel.Text = "No record matches that ID."
            Return
        End If

        row("Name") = nameInput.Text.Trim()
        statusLabel.Text = $"Record {recordId} updated."
    End Sub

    Private Sub DeleteRecord(sender As Object, e As EventArgs)
        Dim recordId As Integer
        If Not Integer.TryParse(recordIdInput.Text, recordId) Then
            statusLabel.Text = "Enter a valid record ID to delete."
            Return
        End If

        Dim row As DataRow = records.Rows.Find(recordId)
        If row Is Nothing Then
            statusLabel.Text = "No record matches that ID."
            Return
        End If

        records.Rows.Remove(row)
        statusLabel.Text = $"Record {recordId} deleted."
    End Sub

    Private Sub MoveRecord(offset As Integer)
        If recordGrid.Rows.Count = 0 Then
            statusLabel.Text = "There are no records to navigate."
            Return
        End If

        Dim currentIndex As Integer = If(recordGrid.CurrentRow Is Nothing, 0, recordGrid.CurrentRow.Index)
        Dim newIndex As Integer = Math.Max(0, Math.Min(recordGrid.Rows.Count - 1, currentIndex + offset))
        recordGrid.CurrentCell = recordGrid.Rows(newIndex).Cells(0)
        recordIdInput.Text = Convert.ToString(recordGrid.Rows(newIndex).Cells("ID").Value)
        nameInput.Text = Convert.ToString(recordGrid.Rows(newIndex).Cells("Name").Value)
        statusLabel.Text = $"Current row: {newIndex + 1} of {recordGrid.Rows.Count}."
    End Sub
End Class
