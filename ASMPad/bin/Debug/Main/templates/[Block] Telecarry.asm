;teleports Mario when a certain sprite is carried into it
;by smkdan
;act like whatever
JMP Return : JMP Return : JMP Return : JMP Check : JMP Check : JMP Return : JMP Return
;don't touch those

;sprite to teleport Mario upon carrying into block
!SPRITETELE = $0F

Check:
	PHY		;preserve block hi
	LDY #$0B	;12 sprites to loop through
Loop:
	LDA $14C8,Y	;test sprite status
	CMP #$0B	;must be being carried
	BNE NextSprite
	LDA $009E,y	;test sprite #
	CMP #!SPRITETELE
	BNE NextSprite	;must be equal to specified sprite

;teleport
	SEP #$30
	LDA #$06
	STA $71
	STZ $89
	STZ $88

	BRA ExitLoop

NextSprite:
	DEY		;next sprite
	BPL Loop

ExitLoop:
	PLY	;restore block hi
Return:
	RTL