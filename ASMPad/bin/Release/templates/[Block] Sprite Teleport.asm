;teleports any sprite touching it to a specified point in the level
;by smkdan
;act like 25

;SET THESE!
!XLEVEL = $00C0	;xlocation of level
!YLEVEL = $0170	;ylocation level

;00: not solid for mario
;01: solid for mario
!ISSOLIDMARIO = $01

JMP Mario : JMP Mario : JMP Mario : JMP Sprite : JMP Sprite : JMP Return : JMP Sprite

Mario:
	LDA #!ISSOLIDMARIO	;solid?
	BEQ Return
	LDA #$30		;then act like cement
	STA $1693
	LDY #$01
	RTL

Sprite:
	LDA.b #!XLEVEL		;xlow
	STA $E4,x
	LDA.b #!XLEVEL>>8	;xhigh
	STA $14E0,x

	LDA.b #!YLEVEL		;ylow
	STA $D8,x
	LDA.b #!YLEVEL>>8	;yhigh
	STA $14D4,x

Return:
	RTL