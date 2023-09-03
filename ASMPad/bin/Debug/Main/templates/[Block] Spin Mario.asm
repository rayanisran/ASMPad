;this block spins Mario around

JMP Return : JMP MarioAbove : JMP Return : JMP Return : JMP Return : JMP Return : JMP Return

MarioAbove:
	LDA #$01
	STA $14A6	;spin timer

	LDA $14		;frame counter
	AND #$20
	BEQ Return	;don't process

	LDA $15
	AND #$03	;invert L/R trigger
	BEQ Testheld
	LDA $15
	EOR #$03
	STA $15
Testheld:
	LDA $16
	AND #$03
	BEQ Return
	LDA $16
	EOR #$03	;invert L/R held
	STA $16

Return:
	RTL