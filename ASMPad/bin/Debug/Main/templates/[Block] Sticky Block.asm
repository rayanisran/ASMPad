;mario cannot jump from this block
;by smkdan

JMP Return : JMP MarioAbove : JMP Return : JMP Return : JMP Return : JMP Return : JMP Return

MarioAbove:
	LDA #$80	;reset 'b' on path pad data
	TRB $16
	TRB $18
Return:
	RTL