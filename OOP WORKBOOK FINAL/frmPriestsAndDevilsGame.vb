Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO

' ============================================================
'  CLASS 1: GameState  (game logic only - no user interface)
' ============================================================
Public Class GameState
    Private _mLeft As Integer
    Private _cLeft As Integer
    Private _mRight As Integer
    Private _cRight As Integer
    Private _boatOnLeft As Boolean
    Private _moves As Integer

    Public Sub New()
        ResetGame()
    End Sub

    Public ReadOnly Property MissionariesLeft As Integer
        Get
            Return _mLeft
        End Get
    End Property

    Public ReadOnly Property CannibalsLeft As Integer
        Get
            Return _cLeft
        End Get
    End Property

    Public ReadOnly Property MissionariesRight As Integer
        Get
            Return _mRight
        End Get
    End Property

    Public ReadOnly Property CannibalsRight As Integer
        Get
            Return _cRight
        End Get
    End Property

    Public ReadOnly Property BoatOnLeft As Boolean
        Get
            Return _boatOnLeft
        End Get
    End Property

    Public ReadOnly Property Moves As Integer
        Get
            Return _moves
        End Get
    End Property

    Public Sub ResetGame()
        _mLeft = 3
        _cLeft = 3
        _mRight = 0
        _cRight = 0
        _boatOnLeft = True
        _moves = 0
    End Sub

    ' Is it allowed to put m missionaries and c cannibals in the boat?
    Public Function IsValidMove(m As Integer, c As Integer) As Boolean
        Dim total As Integer = m + c
        If m < 0 OrElse c < 0 Then Return False
        If total < 1 OrElse total > 2 Then Return False

        If _boatOnLeft Then
            Return m <= _mLeft AndAlso c <= _cLeft
        Else
            Return m <= _mRight AndAlso c <= _cRight
        End If
    End Function

    ' Moves the boat. Returns False if the move is not valid.
    Public Function MoveBoat(m As Integer, c As Integer) As Boolean
        If Not IsValidMove(m, c) Then Return False

        If _boatOnLeft Then
            _mLeft -= m
            _cLeft -= c
            _mRight += m
            _cRight += c
        Else
            _mRight -= m
            _cRight -= c
            _mLeft += m
            _cLeft += c
        End If

        _boatOnLeft = Not _boatOnLeft
        _moves += 1
        Return True
    End Function

    ' Safe = on each bank, missionaries are not outnumbered (unless there are none).
    Public Function IsSafeState() As Boolean
        Dim leftSafe As Boolean = (_mLeft = 0) OrElse (_mLeft >= _cLeft)
        Dim rightSafe As Boolean = (_mRight = 0) OrElse (_mRight >= _cRight)
        Return leftSafe AndAlso rightSafe
    End Function

    Public Function CheckWin() As Boolean
        Return _mRight = 3 AndAlso _cRight = 3
    End Function
End Class

' ============================================================
'  CLASS 2: CharacterToken  (inherits Control, draws a character)
' ============================================================
Public Class CharacterToken
    Inherits Control

    Public Property IsMissionary As Boolean
    Public Property SlotIndex As Integer
    Public Property OnLeftBank As Boolean = True
    Public Property OnBoat As Boolean = False

    Public Sub New(missionary As Boolean, slot As Integer)
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
        Size = New Size(56, 70)
        Cursor = Cursors.Hand
        IsMissionary = missionary
        SlotIndex = slot
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        If IsMissionary Then
            DrawPriest(g)
        Else
            DrawDevil(g)
        End If
    End Sub

    Private Sub DrawPriest(g As Graphics)
        Using haloPen As New Pen(Color.Gold, 3.0!),
              robeBrush As New SolidBrush(Color.FromArgb(45, 45, 80)),
              skinBrush As New SolidBrush(Color.FromArgb(255, 220, 180)),
              whiteBrush As New SolidBrush(Color.White),
              eyeBrush As New SolidBrush(Color.Black),
              outlinePen As New Pen(Color.FromArgb(30, 30, 50), 2.0!),
              crossPen As New Pen(Color.Gold, 2.0!),
              smilePen As New Pen(Color.Black, 1.5!)
            ' halo
            g.DrawEllipse(haloPen, 15, 1, 26, 9)
            ' robe
            g.FillPolygon(robeBrush, New Point() {New Point(12, 68), New Point(44, 68), New Point(39, 38), New Point(17, 38)})
            g.FillEllipse(robeBrush, 14, 33, 28, 14)
            ' white collar
            g.FillRectangle(whiteBrush, 22, 36, 12, 5)
            ' gold cross
            g.DrawLine(crossPen, 28, 46, 28, 60)
            g.DrawLine(crossPen, 23, 51, 33, 51)
            ' head
            g.FillEllipse(skinBrush, 14, 8, 28, 28)
            g.DrawEllipse(outlinePen, 14, 8, 28, 28)
            ' eyes and smile
            g.FillEllipse(eyeBrush, 20, 19, 4, 5)
            g.FillEllipse(eyeBrush, 32, 19, 4, 5)
            g.DrawArc(smilePen, 21, 22, 14, 9, 20, 140)
        End Using
    End Sub

    Private Sub DrawDevil(g As Graphics)
        Using hornBrush As New SolidBrush(Color.FromArgb(60, 0, 0)),
              bodyBrush As New SolidBrush(Color.FromArgb(200, 30, 30)),
              headBrush As New SolidBrush(Color.FromArgb(225, 50, 50)),
              eyeBrush As New SolidBrush(Color.Yellow),
              pupilBrush As New SolidBrush(Color.Black),
              outlinePen As New Pen(Color.FromArgb(90, 0, 0), 2.0!),
              browPen As New Pen(Color.Black, 2.0!),
              grinPen As New Pen(Color.Black, 1.5!),
              tailPen As New Pen(Color.FromArgb(150, 20, 20), 3.0!)
            ' tail
            g.DrawBezier(tailPen, 40, 62, 54, 64, 54, 50, 49, 45)
            ' body
            g.FillPolygon(bodyBrush, New Point() {New Point(12, 68), New Point(44, 68), New Point(39, 38), New Point(17, 38)})
            g.FillEllipse(bodyBrush, 14, 33, 28, 14)
            ' horns
            g.FillPolygon(hornBrush, New Point() {New Point(15, 15), New Point(13, 0), New Point(24, 9)})
            g.FillPolygon(hornBrush, New Point() {New Point(41, 15), New Point(43, 0), New Point(32, 9)})
            ' head
            g.FillEllipse(headBrush, 14, 8, 28, 28)
            g.DrawEllipse(outlinePen, 14, 8, 28, 28)
            ' angry eyes
            g.FillEllipse(eyeBrush, 19, 18, 7, 6)
            g.FillEllipse(eyeBrush, 30, 18, 7, 6)
            g.FillEllipse(pupilBrush, 22, 19, 3, 4)
            g.FillEllipse(pupilBrush, 31, 19, 3, 4)
            g.DrawLine(browPen, 18, 16, 26, 19)
            g.DrawLine(browPen, 38, 16, 30, 19)
            ' evil grin
            g.DrawArc(grinPen, 20, 21, 16, 11, 20, 140)
        End Using
    End Sub
End Class

' ============================================================
'  CLASS 3: SceneCanvas  (inherits Panel, removes flicker)
' ============================================================
Public Class SceneCanvas
    Inherits Panel

    Public Sub New()
        DoubleBuffered = True
    End Sub
End Class

' ============================================================
'  CLASS 4: BoatControl  (a real-looking wooden boat)
'  - If a file called "boat.png" is next to the .exe, that image is used.
'  - Otherwise the boat is drawn with GDI+ (wooden hull, planks, oars).
' ============================================================
Public Class BoatControl
    Inherits Control

    Private _image As Image = Nothing

    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
        Size = New Size(200, 76)

        Try
            Dim path As String = System.IO.Path.Combine(Application.StartupPath, "boat.png")
            If File.Exists(path) Then _image = Image.FromFile(path)
        Catch
            _image = Nothing
        End Try
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        If _image IsNot Nothing Then
            g.DrawImage(_image, 0, 0, Width, Height)
            Return
        End If

        ' ---- hull shape (curved bow and stern) ----
        Using hull As New GraphicsPath()
            hull.AddLine(0, 26, 200, 26)
            hull.AddBezier(200, 26, 196, 52, 178, 68, 150, 70)
            hull.AddLine(150, 70, 50, 70)
            hull.AddBezier(50, 70, 22, 68, 4, 52, 0, 26)
            hull.CloseFigure()

            Using hullBrush As New LinearGradientBrush(New Rectangle(0, 26, 200, 46), Color.FromArgb(160, 100, 55), Color.FromArgb(85, 48, 22), LinearGradientMode.Vertical),
                  outlinePen As New Pen(Color.FromArgb(50, 28, 10), 2.5!),
                  plankPen As New Pen(Color.FromArgb(110, 60, 28, 8), 1.5!),
                  rimBrush As New SolidBrush(Color.FromArgb(200, 140, 80)),
                  rimPen As New Pen(Color.FromArgb(70, 40, 15), 2.0!),
                  oarBrush As New SolidBrush(Color.FromArgb(120, 75, 35)),
                  oarPen As New Pen(Color.FromArgb(120, 75, 35), 4.0!),
                  foamPen As New Pen(Color.FromArgb(190, 255, 255, 255), 2.5!)

                g.FillPath(hullBrush, hull)

                ' wood planks (clipped to the hull)
                Dim oldClip As Region = g.Clip
                g.SetClip(hull)
                g.DrawLine(plankPen, 0, 40, 200, 40)
                g.DrawLine(plankPen, 0, 54, 200, 54)
                For x As Integer = 20 To 180 Step 40
                    g.DrawLine(plankPen, x, 26, x + 6, 70)
                Next
                g.Clip = oldClip

                g.DrawPath(outlinePen, hull)

                ' top rim (gunwale)
                g.FillRectangle(rimBrush, 0, 22, 200, 8)
                g.DrawRectangle(rimPen, 0, 22, 200, 8)

                ' seat planks
                g.FillRectangle(oarBrush, 30, 34, 55, 5)
                g.FillRectangle(oarBrush, 115, 34, 55, 5)

                ' oars sticking out of the sides
                g.DrawLine(oarPen, 62, 28, 28, 60)
                g.FillEllipse(oarBrush, 18, 56, 16, 8)
                g.DrawLine(oarPen, 138, 28, 172, 60)
                g.FillEllipse(oarBrush, 166, 56, 16, 8)

                ' foam at the waterline
                g.DrawArc(foamPen, 16, 62, 168, 14, 10, 160)
            End Using
        End Using
    End Sub
End Class

' ============================================================
'  CLASS 5: the game window (Week 19 - Animation)
' ============================================================
Public Class frmPriestsAndDevilsGame
    Inherits Form

    Private Const BoatY As Integer = 320
    Private Const LeftDockX As Integer = 250
    Private Const RightDockX As Integer = 550
    Private Const HopSteps As Integer = 12

    Private ReadOnly game As New GameState()
    Private ReadOnly scene As New SceneCanvas()
    Private ReadOnly boat As New BoatControl()
    Private ReadOnly tokens(5) As CharacterToken    ' 0-2 = missionaries, 3-5 = cannibals
    Private ReadOnly passengers As New List(Of CharacterToken)()   ' who is on the boat (in seat order)

    Private ReadOnly titleLabel As New Label()
    Private ReadOnly statusLabel As New Label()
    Private ReadOnly movesLabel As New Label()
    Private ReadOnly infoBox As New GroupBox()
    Private ReadOnly infoLabel As New Label()

    Private WithEvents startButton As New Button()
    Private WithEvents moveButton As New Button()
    Private WithEvents resetButton As New Button()
    Private WithEvents moveTimer As New System.Windows.Forms.Timer With {.Interval = 15}
    Private WithEvents hopTimer As New System.Windows.Forms.Timer With {.Interval = 15}

    Private gameActive As Boolean = False
    Private isMoving As Boolean = False
    Private isHopping As Boolean = False
    Private boatTargetX As Integer = LeftDockX

    ' hop animation data
    Private hopToken As CharacterToken
    Private hopFrom As Point
    Private hopTo As Point
    Private hopStep As Integer
    Private hopBoarding As Boolean

    ' ---------------- constructor ----------------
    Public Sub New()
        Text = "3 Priests and 3 Devils"
        ClientSize = New Size(1000, 780)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        BackColor = Color.FromArgb(18, 30, 52)

        BuildUi()
        ResetEverything()
    End Sub

    Private Sub BuildUi()
        titleLabel.Text = "3 PRIESTS && 3 DEVILS"
        titleLabel.SetBounds(0, 0, 1000, 56)
        titleLabel.Font = New Font("Segoe UI", 24.0!, FontStyle.Bold)
        titleLabel.ForeColor = Color.White
        titleLabel.TextAlign = ContentAlignment.MiddleCenter
        Controls.Add(titleLabel)

        scene.SetBounds(0, 56, 1000, 470)
        AddHandler scene.Paint, AddressOf Scene_Paint
        Controls.Add(scene)

        ' the boat (custom-drawn control)
        scene.Controls.Add(boat)

        ' six tokens
        For i As Integer = 0 To 5
            tokens(i) = New CharacterToken(i < 3, i)
            AddHandler tokens(i).Click, AddressOf Token_Click
            scene.Controls.Add(tokens(i))
            tokens(i).BringToFront()
        Next

        ' buttons
        SetupButton(startButton, "Start Game", 20, 545)
        SetupButton(moveButton, "Move Boat", 195, 545)
        SetupButton(resetButton, "Reset Game", 370, 545)

        statusLabel.SetBounds(20, 610, 520, 70)
        statusLabel.Font = New Font("Segoe UI", 12.0!, FontStyle.Bold)
        statusLabel.ForeColor = Color.White
        Controls.Add(statusLabel)

        movesLabel.SetBounds(20, 690, 300, 30)
        movesLabel.Font = New Font("Segoe UI", 13.0!, FontStyle.Bold)
        movesLabel.ForeColor = Color.Gold
        Controls.Add(movesLabel)

        ' rules
        infoBox.Text = "Rules"
        infoBox.SetBounds(560, 535, 420, 190)
        infoBox.ForeColor = Color.White
        infoLabel.Location = New Point(10, 22)
        infoLabel.Size = New Size(400, 155)
        infoLabel.Font = New Font("Segoe UI", 10.0!)
        infoLabel.ForeColor = Color.White
        infoLabel.Text = "1. Click a character to put them on the boat (click again to get off). The boat carries 1 or 2 people." & Environment.NewLine & Environment.NewLine &
            "2. The boat cannot move empty." & Environment.NewLine & Environment.NewLine &
            "3. Priests must never be outnumbered by devils on a bank (unless no priests are there)." & Environment.NewLine & Environment.NewLine &
            "4. Get all 3 priests and 3 devils to the right bank to win."
        infoBox.Controls.Add(infoLabel)
        Controls.Add(infoBox)
    End Sub

    Private Sub SetupButton(b As Button, caption As String, x As Integer, y As Integer)
        b.Text = caption
        b.SetBounds(x, y, 160, 46)
        b.BackColor = Color.FromArgb(27, 75, 125)
        b.ForeColor = Color.White
        b.FlatStyle = FlatStyle.Flat
        b.Font = New Font("Segoe UI", 11.0!, FontStyle.Bold)
        Controls.Add(b)
    End Sub

    ' ---------------- drawing the river scene ----------------
    Private Sub Scene_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Using skyBrush As New LinearGradientBrush(New Rectangle(0, 0, 1000, 200), Color.FromArgb(255, 214, 140), Color.FromArgb(120, 180, 230), LinearGradientMode.Vertical),
              leftBrush As New SolidBrush(Color.FromArgb(70, 140, 60)),
              rightBrush As New SolidBrush(Color.FromArgb(150, 110, 60)),
              riverBrush As New SolidBrush(Color.FromArgb(50, 120, 200)),
              ripplePen As New Pen(Color.FromArgb(150, 255, 255, 255), 2),
              labelBrush As New SolidBrush(Color.White),
              labelFont As New Font("Segoe UI", 10.0!, FontStyle.Bold)
            g.FillRectangle(skyBrush, 0, 0, 1000, 200)
            g.FillRectangle(leftBrush, 0, 200, 240, 270)
            g.FillRectangle(rightBrush, 760, 200, 240, 270)
            g.FillRectangle(riverBrush, 240, 200, 520, 270)

            For i As Integer = 0 To 4
                g.DrawArc(ripplePen, 280 + i * 95, 400 + (i Mod 2) * 30, 60, 12, 0, 180)
            Next

            g.DrawString("LEFT BANK (start)", labelFont, labelBrush, 15, 203)
            g.DrawString("RIVER", labelFont, labelBrush, 475, 203)
            g.DrawString("RIGHT BANK (goal)", labelFont, labelBrush, 770, 203)
        End Using
    End Sub

    ' ---------------- helpers ----------------
    Private Function SlotPoint(slot As Integer, onLeft As Boolean) As Point
        Dim col As Integer = slot Mod 3
        Dim x As Integer = If(onLeft, 20, 775) + col * 72
        Dim y As Integer = If(slot < 3, 225, 305)
        Return New Point(x, y)
    End Function

    ' where a passenger stands on the boat (seat 0 or 1)
    Private Function SeatPoint(seat As Integer) As Point
        Return New Point(boat.Left + 25 + seat * 94, boat.Top - 42)
    End Function

    Private Sub PositionPassengers()
        For i As Integer = 0 To passengers.Count - 1
            passengers(i).Location = SeatPoint(i)
            passengers(i).BringToFront()
        Next
    End Sub

    Private Sub UpdateMoves()
        movesLabel.Text = "Moves: " & game.Moves.ToString()
    End Sub

    Private Sub ShowStatus(message As String)
        statusLabel.Text = "Status: " & message
    End Sub

    ' ---------------- reset / start ----------------
    Private Sub ResetEverything()
        moveTimer.Stop()
        hopTimer.Stop()
        isMoving = False
        isHopping = False
        gameActive = False
        game.ResetGame()
        passengers.Clear()

        boat.Location = New Point(LeftDockX, BoatY)
        boat.BringToFront()
        For i As Integer = 0 To 5
            tokens(i).OnLeftBank = True
            tokens(i).OnBoat = False
            tokens(i).Location = SlotPoint(i, True)
            tokens(i).BringToFront()
        Next

        startButton.Enabled = True
        moveButton.Enabled = False
        UpdateMoves()
        ShowStatus("Press Start Game to begin.")
    End Sub

    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        gameActive = True
        startButton.Enabled = False
        moveButton.Enabled = True
        ShowStatus("Click up to 2 characters to put them on the boat, then press Move Boat.")
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles resetButton.Click
        ResetEverything()
    End Sub

    ' ---------------- clicking a character = hop on / hop off the boat ----------------
    Private Sub Token_Click(sender As Object, e As EventArgs)
        If Not gameActive OrElse isMoving OrElse isHopping Then Return

        Dim t As CharacterToken = DirectCast(sender, CharacterToken)

        If t.OnBoat Then
            ' hop back to the bank where the boat is docked
            passengers.Remove(t)
            t.OnBoat = False
            t.OnLeftBank = game.BoatOnLeft
            PositionPassengers()
            StartHop(t, SlotPoint(t.SlotIndex, t.OnLeftBank), False)
            ShowStatus("Got off the boat. Passengers: " & passengers.Count.ToString() & " of 2.")
        Else
            If t.OnLeftBank <> game.BoatOnLeft Then
                ShowStatus("That character is on the other bank. Click one on the boat's bank.")
                Return
            End If
            If passengers.Count >= 2 Then
                ShowStatus("The boat holds only 2 characters.")
                Return
            End If
            ' hop onto the boat right away
            t.BringToFront()
            StartHop(t, SeatPoint(passengers.Count), True)
            ShowStatus("Boarding... Press Move Boat when ready.")
        End If
    End Sub

    Private Sub StartHop(t As CharacterToken, target As Point, boarding As Boolean)
        hopToken = t
        hopFrom = t.Location
        hopTo = target
        hopStep = 0
        hopBoarding = boarding
        isHopping = True
        t.BringToFront()
        hopTimer.Start()
    End Sub

    Private Sub HopTimer_Tick(sender As Object, e As EventArgs) Handles hopTimer.Tick
        hopStep += 1
        Dim f As Double = hopStep / HopSteps
        Dim x As Integer = CInt(hopFrom.X + (hopTo.X - hopFrom.X) * f)
        Dim y As Integer = CInt(hopFrom.Y + (hopTo.Y - hopFrom.Y) * f - Math.Sin(Math.PI * f) * 40)
        hopToken.Location = New Point(x, y)

        If hopStep >= HopSteps Then
            hopTimer.Stop()
            hopToken.Location = hopTo
            If hopBoarding Then
                hopToken.OnBoat = True
                passengers.Add(hopToken)
                PositionPassengers()
                ShowStatus("Passengers: " & passengers.Count.ToString() & " of 2. Press Move Boat when ready.")
            End If
            isHopping = False
        End If
    End Sub

    ' ---------------- moving the boat ----------------
    Private Sub MoveButton_Click(sender As Object, e As EventArgs) Handles moveButton.Click
        If Not gameActive OrElse isMoving OrElse isHopping Then Return

        Dim m As Integer = 0
        Dim c As Integer = 0
        For Each t As CharacterToken In passengers
            If t.IsMissionary Then m += 1 Else c += 1
        Next

        If m + c = 0 Then
            ShowStatus("Click 1 or 2 characters to board the boat first.")
            Return
        End If
        If Not game.IsValidMove(m, c) Then
            ShowStatus("That move is not allowed.")
            Return
        End If

        boatTargetX = If(game.BoatOnLeft, RightDockX, LeftDockX)
        isMoving = True
        moveButton.Enabled = False
        ShowStatus("Sailing across the river...")
        moveTimer.Start()
    End Sub

    Private Sub MoveTimer_Tick(sender As Object, e As EventArgs) Handles moveTimer.Tick
        Dim stepSize As Integer = 6
        If boat.Left < boatTargetX Then
            boat.Left = Math.Min(boat.Left + stepSize, boatTargetX)
        ElseIf boat.Left > boatTargetX Then
            boat.Left = Math.Max(boat.Left - stepSize, boatTargetX)
        End If
        PositionPassengers()

        If boat.Left = boatTargetX Then
            moveTimer.Stop()
            FinishCrossing()
        End If
    End Sub

    Private Sub FinishCrossing()
        Dim m As Integer = 0
        Dim c As Integer = 0
        For Each t As CharacterToken In passengers
            If t.IsMissionary Then m += 1 Else c += 1
        Next

        game.MoveBoat(m, c)
        Dim arrivedLeft As Boolean = game.BoatOnLeft

        ' everyone steps off onto the bank the boat arrived at
        For Each t As CharacterToken In passengers
            t.OnBoat = False
            t.OnLeftBank = arrivedLeft
            t.Location = SlotPoint(t.SlotIndex, arrivedLeft)
        Next
        passengers.Clear()

        isMoving = False
        UpdateMoves()

        If Not game.IsSafeState() Then
            gameActive = False
            ShowStatus("GAME OVER - the devils outnumber the priests! Press Reset Game.")
            MessageBox.Show(Me, "The devils outnumbered the priests. You lose!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf game.CheckWin() Then
            gameActive = False
            ShowStatus("YOU WIN! Everyone crossed in " & game.Moves.ToString() & " moves.")
            MessageBox.Show(Me, "Everyone crossed safely in " & game.Moves.ToString() & " moves. You win!", "You Win", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            moveButton.Enabled = True
            ShowStatus("Click up to 2 characters to put them on the boat, then press Move Boat.")
        End If
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        moveTimer.Stop()
        hopTimer.Stop()
        MyBase.OnFormClosed(e)
    End Sub
End Class