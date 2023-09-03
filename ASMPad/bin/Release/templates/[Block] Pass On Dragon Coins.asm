;pass on a defined # of yoshi coins
;by smkdan
;act like tile 25
JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP Return : JMP Return : JMP Return : JMP Return
;don't touch those

;amount of yoshi coins the player can pass on
!COINS = $02

MarioAbove:
MarioBelow:
MarioSide:
	LDA $1420	;load coins
	CMP #!COINS	;compare to required coins
	BCS Return	;return (don't act like solid) if carry set

;don't pass
	LDY #$01	;act like 130
	LDA #$30
	STA $1693
Return:
	RTL