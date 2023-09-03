;teleports mario when certain time is reached
;by smkdan

;ONLY USE DECIMAL DIGITS AS YOU WOULD SEE ON THE TIMER
!TIME = $001

JMP Mario : JMP Mario : JMP Mario : JMP Return : JMP Return : JMP Return : JMP Return

Mario:
	LDA $0F33	;load ones
	CMP.b #!TIME&15	;only low nybble
	BNE Return	;if not equal return

	LDA $0F32	;load tens
	CMP.b #!TIME>>4&15
	BNE Return

	LDA $0F31
	CMP.b #!TIME>>8&15
	BNE Return

;teleport
	LDA #$06
	STA $71
	STZ $89
	STZ $88

Return:
	RTL