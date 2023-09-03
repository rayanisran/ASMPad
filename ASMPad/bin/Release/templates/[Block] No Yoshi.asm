;no yoshi block
;by smkdan
;act like 25

;this can be used to deny yoshi access (make solid) or any other given sprite
!DENYSPRITE = $35	;yoshi by default

JMP Mario : JMP Mario : JMP Mario : JMP Sprite : JMP Sprite : JMP Return : JMP Return

Mario:
	LDA $187A	;yoshi flag
	BNE ReturnSolid

	RTL		;not solid for mario if not on yoshi

Sprite:
	LDA $9E,x		;load sprite #
	CMP #!DENYSPRITE	;compare touching sprite to yoshi
	BNE Return		;don't cement for sprites apart from yoshi

ReturnSolid:
	LDA #$30	;cement for yoshi
	STA $1693
	LDY #$01

Return:
	RTL