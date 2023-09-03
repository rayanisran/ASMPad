;End Level on a defined # of coins
;by smkdan (Modified by ICB)
;act like tile 25
JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP Return : JMP Return : JMP Return : JMP Return
;don't touch those. THIS MEANS YOU!! ... Don't Touch 'em!

;amount of coins the player can pass on
!COINS = $20

MarioAbove:
MarioBelow:
MarioSide:
	LDA $0DBF	;load current coins
	CMP #!COINS	;compare to required coins
	BCS Pass  	;Pass (don't act like solid) if carry set

;don't pass
	LDY #$01	;act like 130
	LDA #$30
	STA $1693

Return:			;>the return JMPs above have to be pointed somewhere<		
	RTL		;>don't process pass code<

Pass:
LDA $0DD5
BEQ Next1
BPL $19
Next1: 
LDA #$80
BRA Branch1
Branch1:
LDA #$01
STA $13CE
STA $0DD5
INC $1DE9
LDA #$0B
STA $0100
RTL
LDA $0DD5
BEQ Next2
BPL $19
Next2:
LDA #$80
BRA Branch2
Branch2:
LDA #$01
INC a
STA $13CE
STA $0DD5
INC $1DE9
LDA #$0B
STA $0100
RTL