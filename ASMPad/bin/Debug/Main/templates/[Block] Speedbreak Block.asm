;block that breaks upon reaching given speed
;by smkdan

!BREAKSPEED = $30	;speed block will break on

JMP Return : JMP MarioAbove : JMP Return : JMP Return : JMP Return : JMP Return : JMP Return

MarioAbove:
	LDA $7B
	BPL NoUnsign	;negative?
	EOR #$FF	;two's complement
	INC A
NoUnsign:
	CMP #!BREAKSPEED	;test for breakage
	BCC Return		;return if speed not reached

	PHB			;preserve current bank
	LDA #$02		;push 02
	PHA
	PLB			;bank = 02
	LDA #$00		;default shatter
	JSL $028663		;shatter block
	PLB			;restore bank

	LDA #$02		;block to generate
	STA $9C
	JSL $00BEB0		;generate

Return:
	RTL