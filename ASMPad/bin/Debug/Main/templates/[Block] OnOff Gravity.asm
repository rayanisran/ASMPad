;gravity block with ON/OFF switching
;by smkdan
JMP GravityActive : JMP GravityActive : JMP GravityActive : JMP Return : JMP Return : JMP Return : JMP Return
;don't touch those

;To use this, fill the empty space with one of these blocks 
;Act like 25 ofcourse

;!GRAVITY LEVEL! for each setting
;A value from 00-0F
GRAVITYLEVELS:
	db $08	;gravity OFF
	db $00	;gravity ON

;00-07: Low gravity
;00: low gravity with max effect, 07: low gravity with min effect
;08-0F: High gravity
;08: high gravity with max effect, 0F: high gravity with min effect

;masks
GRVMASK:
db $00,$01,$03,$07,$0F,$1F,$3F,$7F

GravityActive:
	LDX $14AF	;load on/off as index
	LDA GRAVITYLEVELS,x
	PHA		;preserve it

	AND #$07	;only low 3 bits
	TAX		;and into X

	LDA $14		;frame counter
	AND GRVMASK,x	;affect gravity on this frame?
	BNE ReturnP

	LDA $01,s	;load gravity
	BIT #$08	;test for high / low
	BNE HighGravity	

;lowgravity
	DEC $7D		;up
	BRA ReturnP

HighGravity:
	INC $7D		;down
ReturnP:
	PLA		;get gravity level off stack
Return:
	RTL