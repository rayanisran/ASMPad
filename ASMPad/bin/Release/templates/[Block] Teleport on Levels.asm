;teleports Mario when a certain amount of levels have been defeated
;by smkdan
;act like whatever
JMP Check : JMP Check : JMP Check : JMP Return : JMP Return : JMP Return : JMP Return
;don't touch those

;amount of levels to have beaten to teleport
!LEVELSBEAT = $20

Check:
	LDA $1F2E	;load levels beat
	CMP #!LEVELSBEAT
	BCC Return	;return if not enough have been beaten

;teleport
	LDA #$06
	STA $71
	STZ $89
	STZ $88

Return:
	RTL