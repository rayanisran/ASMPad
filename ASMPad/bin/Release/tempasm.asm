;======================================================================================
;The Ultimate n00b boss
;Coded by Iceguy/Iceyoshi
;Please credit if used
;
;A highly customisable boss with configurable health and actions. This boss can have up to 35ish (!) different actions, and
;Can change actions depending on his health. See below for configurations and the readme for more detailed info.
;======================================================================================

!Health 		= $0A   ; Health the sprite has. Can be anywhere between 0-99 (#$63 being 99). Don't be silly, though.
!FaceMario 		= $01 	; Initially face Mario?
				; 01 -> Yes. 00 -> No.

!SprAnimate		= $01	; Does your sprite animate? (i.e. flip between 2 frames)
				; 00 -> No, 01 -> Yes.

!SprAnimate2		= $00   ; Does your sprite animate for JSR Animate2? (In case you want to animate for other frames at a certain HP)
				; 00 -> No, 01 -> Yes.

!SprAnimate3		= $01   ; Does your sprite animate for Animate3?
				; 00 -> No, 01 -> Yes.

				; NOTE: If they animate, you specify the animation frames in the graphics defines below.

!FastAnimation		= $00   ; If you want your sprite to animate fast, make this an 01.
				; If you leave it as 00, the sprite will not animate fast (a bit slower). 
				
!DeathType = $01		; Type of Death:
				; 00 -> Fall down, as if killed by star
				; 01 -> Disappear in a puff of smoke.
				; 02 -> Explosion (bob-omb) style.

!DoFlash = $01			; Does the sprite "flash" when it gets hurt? Flashes through colors and cannot be in
				; Contact if it does.
				; 00 -> No, 01 -> Yes.

!ExitType = $02			; Type of exit when Boss dies:
				; 00 - Goal sphere style, normal exit (no walking).
				; 01 - Goal sphere style, secret exit (walking).
				; 02 - Teleport to whatever in the screen exit.
				; 03 - Fade to OW. (Exit specified below).
				; 04-FF - Nothing.

!TeleSFX = $05			; Sound to play when/if teleporting. ($1DFC)

!ExitSFX = $0B			; Type of music when boss is defeated (only applicable if !Exit is 0-2)

				; 0B - Boss defeated.
				; 0C - Normal SMW level complete music.

!OWExit = $02			; If using !Exit as option 03, fade to the OW..
				; 01 -> Normal exit. 02 = Secret Exit.

!NormalSprite = $02		; Normal sprite to generate (if generating one).
!CustomSprite = $00		; Custom sprite to generate.
				; NOTE: Generating a custom sprite that doesn't exist will (probably) result in a crash.

!Gravity = $10 			; Does your boss float in the air?
				; If so, leave it as 00. Otherwise, this is the gravity for the sprite.
				; Suggested value: any value from 00-50.
				; 10 = low gravity.
				; 50 = high gravity.

!Ledges = $01			; Does your sprite stay on ledges?
				; 00 -> No, 01 -> Yes.

SPRITE_TO_GEN: 			; For generating a random sprite, write the sprites here. Table must be 3 bytes!
 db $0D,$0F,$10			; Bomb, Goomba, ParaGoomba.

!INITSTAT = $01			; STATUS for above sprites. For stationary shells, make this 09 etc.
				; See $14C8,x in the RAM Map for all states.

!StunTimerA = $20		; How long the boss stuns the ground (only applicable if JSR'ing to Strong)
!StunTimerB = $40		; How long the boss stuns the ground (only applicable if JSR'ing to Strong2)


!JumpHeight = $D0		; New Jump height if posioned. $FF = mimimum, 80 = maximum.
				; Advisable - A0-E0

!PoTimer     = $03		; Time to poison Mario, in seconds. (If JSR'ing to Poision).

!Speed	    = $18		; Speed Mario gets when poisioned. 00 = No speed at all, 26 = Max running speed.	
				; Advisable values = 15-20.

!Col = $01			; Have a green face when Mario is poisioned? 00 -> No, 01 -> Yes.
				; NOTES:
				
				; IF NEAR THE STATUS BAR (TOP), THE COLORS REVERT TO NORMAL	
				; SAME IF THE PLAYER PAUSES.
				; THIS AFFECTS COLORS 86,89, 8C and 8E only.

!High = $45			; Color for Mario's face when poisioned.
!Low = $0E			; 4 digit RGB values, like this:
				; XXYY <- YY is !High, and XX is Low.
!High2 = $B6
!Low2 = $30			; Color for Mario's pants.

!FireballHealth = $01		; Does the sprite have fireball HP?
				; 00-> No, 01-> Yes.
				; NOTE-> For fireball HP, it's the normal HP * 3 (this is because it won't be easy	
				; .. to customise)

!CapeHealth = $00		; Does the sprite have cape HP? If so, it's normal health. (Not normal health*3).

!CapeFlight = $00		; Disable cape flight during the boss battle?
				; 00-> No, 01-> Yes.

!ExSound = $26 			; Sound to play when either a bone or hammer is thrown.

		        	; Uses $1DF9.

!PTimer  = $88  		; Timer for p-switch (if it's activated).

!FTimer  = $4F			; How long the sprite flashes for when hit.

!Dark	 = $0B			; Darkness to use when JSR'ing to Dark.
!Dark2   = $07			; Darkness to use when JSR'ing to Dark 2.

				; Can be from 00-0F.
				; 0F - Most brightness (also normal amount in SMW), 00 = Completely black.

!NoSpin = $01			; Should the sprite only be stompable if Mario spin-jumps on him?
				; Stompable like an eerie (bounces off Mario), but it doesn't reduce the boss's HP.
				; NOTE: If enabled, the sprite can be stomped on if using JSR ThrowHurt.
				; 00 -> No, 01-> Yes.

!Bouncy = $00			; "Push" Mario back after jumping on the sprite?
				; 00 -> No, 01-> Yes.

!RiseLoc = $00F0		; If making your sprite rise at a certain hitpoint (JSR Rise)
		 		; ..specify the height at which it should stop.

				; 0145 = 40% up of the screen.
				; 0130 = Halfway up the screen.
				; 0115 = 2/3 up screen.
				; 00F0 = Right below the status bar.
				; Must be 4 digits, otherwise the game will crash!

!RiseSpeed = $E0 		; Speed at which the sprite rises.
		 		; 80 - Extremely fast.
		 		; FF - Slowest.
		 		; Suggested: A0-E0.

!DropSpeed = $16 		; If making your sprite drop again, specify the speed..
		 		; ..at which it falls down.
	         		; 01 -> Slowest.
		 		; 7F -> Extremely fast.
		 		; Suggested values: 10-40.
		 		; NOTE, it stops when it touches the ground.

!PushMario = $22		; Speed to push Mario if the sprite touches him. (Only applicable if doing JSR Push).
!YPush     = $D5		; Upwards push movement if sprite hurts Mario.

;If making your sprite chase Mario..

MaxAcceleration: db $1A,$E6 	; Maximum speeds the sprite can chase.
MaxAccelerationY: db $10,$F0 	; Maximum speeds the sprite can chase.
AccelerationX: db $02,$FE	; Acceleration for X speed.
AccelerationY: db $04,$FC	; Acceleration for Y speed.

;If using messages, read below:

!Msg1T = $01			; Message one type. 01 = Message one, 02 = Message two.
!Msg2T = $01			; Same for the second message.
!Msg1L = $00			; Level of first message.
!Msg2L = $29			; Level of second message.

;These can be quite tricky. Up to level 24 = 24, but then it's 25 for level 101. 101 = 25, 102 = 26, 103 = 27, 104 = 28,
;105 = 29, 106 = 2A, 107 = 2B etc.

; Level 00 = 00
; Level 1A = 1A 
; Level 20 = 20
; etc.

; Level 24 = 24
; Level 101 = 25 ; After level 24 comes level 101, so that becomes 25.
; Level 102 = 26
; Level 103 = 27.
;================================================================
;Status Bar Stuff Here!
;================================================================

!A = $0A : !B = $0B : !C = $0C : !D = $0D : !E = $0E : !F = $0F : !G = $10 : !H = $11 : !I = $12 : !J = $13 : !K = $14
!L = $15 : !M = $16 : !N = $17 : !O = $18 : !P = $19 : !Q = $1A : !R = $1B : !S = $1C : !T = $1D : !U = $1E : !V = $1F
!W = $20 : !X = $21 : !Y = $22 : !Z = $23
!YMove = $1510
!InvTimer = $7F9989 ; Used for many sprite misc. stuff. Make it atleast 12 bytes.
;Don't touch that.

!ShowTiles	= $01		; Draw <Name>x<HP> on status bar? (E.g. Idiotx10)
				; 00 -> No. Anything else -> Yes.

NameTable:
	db !S,!M,!W,!C		; Name of boss. Write like this: !A for A, !F for F, !Q for Q etc.
				; MUST PUT THE ! AND COMMA (,) after each letter except the last one.
				; The db at the beginning is important, don't touch that.

				; db !F,!A,!I,!L will write "FAIL".

!NameSize = $03			; The number of letters you wrote for your boss -1. For example, If my boss was called
				; LOL, I would put a 02 here (3-1). (NOTE: must be 2 digit, so for 04 write 04, not 4)

!Position = $0EF9		; Position to draw tiles to.
				; Can be anywhere from $0EF9-$0F2F. See 1024's status bar for reference.

;================================================================
;Graphics Routine!
;================================================================

;Notes:

;This is a 32x32 Boss, so it has 4 tiles - Top Left, Top Right, Bottom Left and Bottom Right.
;If you didn't set your sprite to animate, it will NOT use the walking frames.

;By default, it will always animate between frames if it's set to animate.

;For each 16x16 tile (Top Right, Top Left etc.) , you specify the top-left part of that 16x16 tile. For example, the Mushroom
;Is a 16x16 tile. In the 8x8 editor it's tile 0x224. So you would right the last 2 digits - 24 for that and the whole 16x16 
;Gets drawn. Similarly, the fire flower graphic would be 26.

;NOTE: IF YOU CHOOSE "use second graphics page" in the cfg editor, it uses tiles from SP3 and SP4.

!TopLeft = $A0
!TopRight = $AB
!BottomLeft = $C0
!BottomRight = $C2

!WalkTopLeft = $E0
!WalkTopRight = $E2
!WalkBottomLeft = $E4
!WalkBottomRight = $E6

;If using more animation frames in the sprite, specify them here:
;NOTE: To use them, use JSR Animate2 and JSR Animate3 in your sprite STATES.

;For Animate2:

!TopLeft2 = $CC
!TopRight2 = $CE
!BottomLeft2 = $EC
!BottomRight2 = $EE

!WalkTopLeft2 = $6D
!WalkTopRight2 = $6D
!WalkBottomLeft2 = $6D
!WalkBottomRight2 = $6D

;For Animate3:

!TopLeft3 = $6D
!TopRight3 = $6D
!BottomLeft3 = $6D
!BottomRight3 = $6D

!WalkTopLeft3 = $CC
!WalkTopRight3 = $CE
!WalkBottomLeft3 = $EC
!WalkBottomRight3 = $EE

;================================================================
;Sprite X Speeds Are Configurable Here!
;================================================================

; Notes:
; If your sprite has 6HP, you need to write to the first 7 values of the table only (00-06), if it has 3 HP, write to the first 4 values of the table etc.
; Slowest speed is 01 and fastest is 7F. It's recommended to not go higher than 25 as it gets very fast from there onwards.
; 00 is stationary i.e. the sprite doesn't move.
; First value in the table is for 00 HP, last value is for 99 HP. Writing to values not used doesn't have any affect.

Tbl1R:  db $00,$11,$11,$12,$14,$10,$0F,$13,$18,$14,$10 ; 10
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 20
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 30
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 40
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 50
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 60
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 70
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 80
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 90
	db $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF ; 99

;===================;
;    Pointers	    ;
;===================;

MainPointer:
	LDA $1528,x
	JSL $0086DF
	dw STATE0 ; Dead state. Don't use for actions (ony RTS)
	dw STATE1 ; 01, last hit point.
	dw STATE2 ; 02, 2 hit points left ..
	dw STATE3 ; 03, 3rd last hit point.
	dw STATE4 ; 04, etc.
	dw STATE5 ; 05
	dw STATE6 ; 06
	dw STATE7 ; 07
	dw STATE8 ; NOTE! MAKE SURE YOU HAVE AS MANY DWS AS YOUR BOSS HP, OTHERWISE YOU MIGHT GET PROBLEMS!
	dw STATE9
	dw STATE10

STATE10:
STATE9:
STATE8:
STATE7:
STATE6:
STATE5:
STATE4:
STATE3:
STATE2:
STATE1:
STATE0:
JSR GenBullets
RTS

;==================================================================
;OPTION POINTERS ARE HERE!
;Possible Options:
;
;JSR SpitFire ; Makes the sprite spit fireballs (Ext. sprite) at Mario.
;JSR Roar ; Roars and throws a bunch of hammers at once.
;JSR GenParaBomb ; This will make the sprite turn on the parabomb generator.
;JSR GenFire ; This will make the sprite turn on the Bowser spit fire generator.
;JSR GenBullets ; This will make the sprite turn on the normal bullet bill generator.
;JSR Chase ; Use this to make the sprite chase Mario, both horizontally and vertically.
	   ; NOTE -> TO USE THIS, YOU MUST ALSO USE JSR Move. This is because updating the sprite position is in that code.

;JSR NoChase ; Use this to clear out the chase effect. NEEDED if you want to cancel out the chase effect.

;JSR LoopyLight ; Makes the screen loop through brightness, so the screen revolves around brightness and darkness.
		; NOTE! DO NOT USE THIS IN THE FIRST SPRITE STATE (WHEN HE HAS FULL HP), THIS CAN MESS UP THE MOSAIC
		; EFFECT IF YOU DO SO! (If you've disabled it, I guess it's fine though.)
		; This is kind of a stupid option, but I added it anyways, because I was bored >_>

;JSR Push ; When Mario touches the sprite, it hurts him and pushes him away.
;JSR Animate2 ; Makes the sprite use different animations.
;JSR Animate3 ; Makes the sprite use more different animations.
;JSR Animate ; Use this to make the sprite change back to it's default animations (only needed if changing).
;JSR WavyMotion ; Makes the sprite move up and down in a wavy motion.
;JSR BoneA ; Makes the sprite throw a bone.
;JSR BoneB ; Makes the sprite throw a bone more frequently.
;JSR HammerA ; Makes the sprite throw a hammer.
;JSR HammerB ; Makes the sprite throw a hammer more frequently.
;JSR Move ; Makes the sprite walk. This makes it move with speeds in Tbl1. (Top of the file to configure).
;JSR Rise ; Makes the sprite rise up to a configurable height (see top of file).
;JSR Drop ; Makes the sprite drop down to the ground (see top of file for drop speed).
;JSR JumpA ; Makes the sprite jump every 4 seconds.
;JSR JumpB ; Makes the sprite jump every 3 seconds.
;JSR JumpC: Makes the sprite jump every 2 seconds.
;JSR ClearJump ; Use this to "clear" the jumping effect. It's needed if you want to cancel out the sprite jump effect.
;JSR Strong ; Makes the sprite stun the ground after jumping (only applies if jumping).
;JSR Strong2 ; Same as Strong, but stuns the ground for more time.
;JSR OnOff ; Makes the sprite turn off the P-Switch. Using ON/OFF custom blocks, make enemies spawn from above!
;JSR PSwitch ; Makes the sprite turn on the P-switch. DONE!
;JSR Follow ; Makes the sprite move towards Mario all the time.
;JSR JumpHurt ; Makes the sprite get hurt by jumping on it. NOTE: Can't be used with ThrowHurt in the same state!
;JSR ThrowHurt ; Makes the sprite get hurt if something is thrown at him. Can't be used with JumpHurt in the same state!
;JSR GenNSpr ; Makes the sprite throw a normal sprite (configured at the top of the file) at Mario.
;JSR GenCSpr ; Makes the sprite throw a custom sprite (configured at the top of the file) at Mario.
;JSR Dark ; Darkens the screen.
;JSR Dark2 ; Darkens the screen even more.
;JSR Bright ; Makes the screen brightness back to normal (if you want it normal again).
;JSR Msg ; Makes the sprite play a message. Note: recommended you use this only once, because it's the same message.
;JSR Msg2 ; Makes the sprite play another message. Note: recommended you use this only once, because it's the same message.
;JSR Inv ; Makes the sprite invisible for around 3 seconds. When invisible, he cannot be hurt.
;JSR ClearInv ; Use this to "clear" the invisiblity effect. It's needed if you want to cancel out the effect at some HP.
;JSR RandMove ; Makes the sprite change direction every once in a while.
;JSR RandSpr ; Makes the sprite throw a random sprite out of ANY 2 SMW ORIGINAL SPRITES.
;JSR Poision 
	     ; For X seconds (Whatever poision time you wrote): 
	     ; Reduces Mario's jumping height. stops running, changes his colors to green.

;==================================================================
;SPRITE MAIN CODES!	
;==================================================================

;Symbolic Addresses

	!JumpTimer = $1504
	!BounceCheck = $C2
	!Flash = $1594
	!Poision = $1504

FlashTable:
	db $08,$08,$0C,$0C,$0C,$06,$06,$06

print "INIT ",pc
	LDA #!Health ; Store health.
	STA $1528,x
	LDA #!FaceMario ; If he's set to 00 ..
	CMP #$00
	BEQ NoFace
	JSR SUB_HORZ_POS ; Make him face Mario.
	TYA ; Get the sprite's direction relative to Mario into A.
	EOR #$01 ; Why is this needed?? (ledges)
	STA $157C,x ; Store new direction.
NoFace:
	PHX
	LDX #$0B
DecDec:
	LDA #$00
	STA !InvTimer,x
	DEX ; When riding, clipping values must be 0E AE 0E 0E.
	BPL DecDec ; lulz, this loop wastes a lot of v-blank time.
	PLX ; So dsx.asm will compete with it.
	RTL
;==================================================================
;Sprite Main INIT	
;==================================================================
Offset:
	db $00,$10
BounceSpeed:
	db $0B,$F2
BounceMarioX:
	db $E4,$1B

print "MAIN ",pc
	PHB
	PHK ; Always change the data bank.
	PLB
	;JSR Graphics
	JSR RunSpr
	JSR DecrTimers
	PLB
	RTL ; End the code after jumping to sprite main.

DecrTimers:
	LDA !BounceCheck,x
	BEQ +
	DEC !BounceCheck,x
+
	LDA $163E,x
	BEQ +
	LDA $13
	AND #$3F
	BNE +
	DEC $163E,x
+
	LDA !Flash,x
	BEQ +
	DEC !Flash,x
+
	LDA !Poision,x
	BEQ +
	JSR DoAttack
	LDA $13
	AND #$3F	; Decrease poision timer every second.
	BNE +
	DEC !Poision,x
+
	RTS
DoAttack:
	LDA $16
	ORA $18	; If pressing the jump button ..
	AND #$80
	BEQ +
	LDA $77
	AND #$04 ; .. and on ground..
	BEQ +
++
	LDA #!JumpHeight
	STA $7D ; limit height.
+
	JSR LimitXSpd
	LDA #!Col
	CMP #$01
	BNE +
	JSR ChangePals
+
GoAway:
	RTS
;==================================================================
;Sprite Main Routine	
;==================================================================
RunSpr:
	JSR Graphics
	LDA $14C8,x
	CMP #$08
	BNE GoAway ; Return if sprite dead.
	LDA $9D
	BNE GoAway ; Or locked,
	
	LDA !InvTimer+9

	; 00 -> Animation 1.
	; 01 -> Animation 2.
	; 02 -> Animation 3.

	ASL A : PHX : TAX : JMP (AniPtr,x) : AniPtr:
	
	dw Ani1 ; 00
	dw Ani2 ; 01
	dw Ani3 ; 02
	
Ani1:
	PLX
	LDA #!SprAnimate ; IF 01 ..
	BRA DoAniCheck
Ani2:
	PLX
	LDA #!SprAnimate2 ; IF 01 ..
	BRA DoAniCheck
Ani3:
	PLX
	LDA #!SprAnimate3 ; IF 01 ..

DoAniCheck:
	CMP #$01 ; If it's NOT 01 ..
	BNE SkipAn ; Skip animation code.

        LDA $14
        LSR A
	LSR A
	LSR A
	PHA
	LDA #!FastAnimation ; Check to see is the user wants fast animation.
	CMP #$01 ; Also, I use Y instead of A so $14 doesn't mess up. No, I push A.
	BEQ +
	PLA
        LSR A ; Else, stick another LSR there.
	BRA ++
+
	PLA
++
        CLC
        ADC $15E9 ; Flip frames when needed.
        AND #$01
        ASL A
        ASL A  
        STA $1602,x	
SkipAn:
	LDA $1528,x
	BNE NormalRoutines
	STZ !Position+!NameSize+3
	LDA #$FC
	STA !Position+!NameSize+2	
	JMP KillSprite
NormalRoutines:
	LDA #!ShowTiles ; .. If it's set to show tiles.
	BEQ NoDont
	PHX 
	JSR DrawTilemaps ; Draw the tilemaps.
	PLX
	LDA $1528,x
	PHX
	JSR HexDec
	STX !Position+!NameSize+2
	STA !Position+!NameSize+3
	PLX
NoDont:
	LDA $1588,x		; Add all basic stuff.
	AND #$03		; Such as flipping when hitting a wall.
	BEQ NoFlipping
	LDA $157C,x
	EOR #$01
	STA $157C,x
	TAY
	LDA BounceSpeed,y
	STA $B6,x
Apply:  
	JSL $01802A		; Prevents a glitch.
	LDA #$10		; Where the sprite runs into a wall.
	STA !BounceCheck,x
NoFlipping:
	LDA #!FireballHealth
	CMP #$00
	BEQ NoHit	; If user specified not to give fireball HP, return.
	JSR FireballHP
	BEQ NoHit 	; Return if there's no fireball/sprite contact.
	LDA !Flash,x
	BNE NoHit
	LDA !InvTimer
	CMP #$02
	BCS NoHit

	LDA !InvTimer+4 ; If fireball hits != 2 ..
	CMP #$02
 	BNE +
	LDA #$FF
	STA !InvTimer+4 ; Reset the flag.
	DEC $1528,x
+
	LDA #!FTimer
	STA !Flash,x	; Store flash timer.
	LDA !InvTimer+4
	INC A
	STA !InvTimer+4
	RTS
NoHit:
	LDA #!CapeHealth ; If CHP = 01 ..
	BEQ NoCapeHP
	JSR SCCR
	BEQ NoCapeHP
	LDA !Flash,x
	BNE NoCapeHP
	LDA #!FTimer
	STA !Flash,x
	DEC $1528,x
NoCapeHP:
	LDA #!CapeFlight
	BEQ +
	STZ $149F
+
	LDA #!Ledges
	CMP #$01
	BNE JumpMain ; Skip ledges if it's set to 00.
	LDA $1588,x
	ORA !InvTimer+8
	BNE OnGround
	LDA $157C,x
	EOR #$01
	STA $157C,x
	LDA #$01
	STA !InvTimer+8
OnGround:
	LDA $1588,x
	AND #$04
	BEQ JumpMain
	LDA #$00
	STA !InvTimer+8
JumpMain:
	JMP MainPointer
;==================================================================
;Carry Sprite Code (Not finished yet, reserved for future updates)
;==================================================================
!Temp = $00	; Where to temporarily store proximity range.

CarryHurt:
	LDA !InvTimer+2
	STA $0F13
	ASL A : PHX : TAX : JMP (CarryPtr,x)
CarryPtr:
	dw ProxRange	; 00 ; Determine range.
	dw IsCarry	; 01 ; Carrying.
	dw Thrown	; 02 ; Thrown.
	dw Falling	; 03 ; Falling from throw.
Falling:
	PLX			; Restore index.
	JSL $01802A
	LDA $1588,x		;\
	AND #$04		; | If on the ground now ..
	BEQ Return		;/ .. and not before ..
	LDA #$10		;\
	STA $1887		;/ Shake ground.
	LDA #$00		;\
	STA !InvTimer+2		;/ State = 0.
	DEC $1528,x		; HP = (HP-1).
Return:	RTS
Thrown:
	PLX
	LDA $76
	EOR #$01
	STA $157C,x
	;LDA $1588,x	
	;AND #$04
	;BEQ +
	LDA #$CA
	STA $AA,x
+	LDY $157C,x
	LDA ThrownXSpeed,y
	STA $B6,x
	JSL $01802A
	LDA #$03
	STA !InvTimer+2
	RTS

ThrownXSpeed: db $10,$F0	
Values: db $F3,$0D
VFix:	db $FF,$00
MarioImgs: db $07,$09
MarioDir: db $01,$00

IsCarry:
	PLX			; Restore index.
	LDA #$01		;\
	STA $148F		;/ Carry item.
	LDY $76			;\
	LDA MarioDir,y		; |
	STA $157C,x		;/ Make sprite relative Mario's direction.
	LDA $94			;\
	CLC			; |
	ADC Values,y		; |
	STA $E4,x		; |
	LDA $95			; | X position of sprite.
	ADC VFix,y		; |
	STA $14E0,x		;/
	LDA $96			;\
	ADC #$0D		; |
	STA $D8,x		; |
	LDA $97			; | Y position of sprite.
	ADC #$00		; |
	STA $14D4,x		;/
	LDA #$07		;\
	STA $13E0		;/ Carry item pose.
	LDA $7B			;\
	BEQ +			; |
	LDA $13			; | If walking, flip between pose 7 and 9.
	LSR #3			; | LSR = slower animation rate.
	AND #$01		; | # of frames + 1.
	TAY			; |
	LDA MarioImgs,y		; |
	STA $13E0		;/
+	LDA $7B			;\
	BMI XOR			; |
	LSR			; | Reduce maximum walk speed.
	BRA Label		; |
XOR:	EOR #$FF		; |
	LSR			; |
	EOR #$FF		; |
Label:	STA $7B			;/
	LDA $16			;\
	AND #$40		; | If throwing ..
	BEQ +			; |
	LDA #$02		; | Sprite state = thrown.
	STA !InvTimer+2		;/
+	RTS
	
ProxRange:
	PLX			; Restore index.
	JSR SUB_HORZ_POS	;\
	TYA			; |
	CMP $157C,x		; | Must be behind sprite to carry.
	BEQ NoCarry		;/
	LDA #$16		;\
	STA !Temp		;/ Proximity range = 10.
	JSR ProximityCheck	;\
	BEQ NoCarry		;/ Return if not = 10.
	BCC HurtM		; Hurt if < 10.
	LDA $16			;\
	AND #$40		; | Return if not pressing carry key.
	BEQ NoCarry		;/
	LDA $148F		;\
	BNE NoCarry		;/ Return if already holding item.
	LDA #$01
	STA !InvTimer+2		; = Carrying
	RTS
NoCarry:
	;JSL $01A7DC
	;BCC +
	
	RTS

HurtM:	JSL $01A7DC
	BCC +
	JSL $00F5B7
+	RTS

ProximityCheck:
	LDA $E4,x		;\
	SEC			; |
	SBC $94			; |
	PHA			; |
	JSR SUB_HORZ_POS	; | Check sprite range ..
	PLA			; | (Sprite X Pos - Mario X Pos).
	EOR DirFix,y		; |
	CMP !Temp		; | ; If Range > $1528,x, return. 
	BCS +			; |
	LDA #$01		; | A = #$01.
	RTS			; |
+	LDA #$00		; | A = #$00 if not in range.
	RTS			;/
	
DirFix: db $FF,$00
;==================================================================
;Generator Activation Codes
;==================================================================
GenBullets:
	PHK
	PER NoGGen-1
	PEA $F80E	;\ Push 16-bit RTL address.
	JML $02B07C	;/ Jump to the bullet generation code.
NoGGen:
	RTS
GenFire:
	PHK
	PER NoGGen2-1
	PEA $F80E
	JML $02B036
NoGGen2:
	RTS
GenParaBomb:	; Code has to be written to avoid unwanted stuff.
	LDA $14
	AND #$7F ; Frequency. Can be 7F or FF.
	BNE NoGGen4
	JSL $02A9DE
	BMI NoGGen4
	TYX
	LDA #$08
	STA $14C8,x
	JSL $01ACF9
	LSR
	LDA #$40
	STA $9E,x
	PHK
	PER NoGGen4-1
	PEA $F80E	;\ Push 16-bit RTL address.
	JML $02B34D	;/ Jump to the bullet generation code.
NoGGen4:
	RTS
;StopGen: ; Useless.
	;STZ $18B9	; Don't generate anything.
	;RTS
;==================================================================
;Chase Mario Subroutine
;==================================================================
Chase:
	LDA !InvTimer+1
	CMP #$1F
	BEQ Done
	INC
	STA !InvTimer+1
	RTS
Done:
	JSR SUB_HORZ_POS ; Get sprite's direction relative to Mario ..
	TYA
	STA $157C,x
	TAY
	LDA $B6,x ; .. X speed.
	CMP MaxAcceleration,y ; If going beyond the speed limit, skip.
	BEQ +
	CLC
	ADC AccelerationX,y
	STA $B6,x ; Acceleration = ..wait.
+
	JSR SUB_VERT_POS
	LDA $AA,x
	CMP MaxAccelerationY,y
	BEQ +
	CLC
	ADC AccelerationY,y
	STA $AA,x
+
	LDA #$01
	BRA AccelerateON
NoChase:
	LDA #$00
	STA !InvTimer+1
AccelerateON:
	STA !InvTimer+10
	JSL $01802A
	RTS
;==================================================================
;Loopy Light Routine(s)
;==================================================================
LoopyLight:
	LDA $13 ; $13 -> A.
	LSR #3
	AND #$07 ; Get the low bits of the envmxdizc register..
	TAY ; Into Y.
	LDA Flipper,y
	STA $0DAE
	RTS
Flipper: db $0E,$0A,$08,$06,$03,$00,$00,$00
;==================================================================
;Push Mario Routine
;==================================================================
Push:
	JSL $01A7DC ; Check for Mario/Sprite contact ..
	BCC NoPush ; Return if not any.

	JSR SUB_VERT_POS
	LDA $0E
	CMP #$E6
	BPL SprPush
NoPush:
	RTS
SprPush:
	LDA $1497	
	BNE + ; No immediate push if flashing.
	LDY $76 ; Direction -> Y.
	LDA PushSpd,y ; Accumulate push speeds..
	STA $7B ; Into $7B.
	LDA #!YPush
	STA $7D
	JSL $00F5B7
+
	RTS
PushSpd:
	db !PushMario,$FF-!PushMario
;==================================================================
;Cape HP Routine
;==================================================================
SCCR:
	LDA $13E8
	BEQ SCCRNOCONTACT
	LDA $15D0,x
	ORA $154C,x
	ORA $1FE2,x
	BNE SCCRNOCONTACT
	LDA $1632,x
	PHY
	LDY $74
	BEQ SCCRLABEL1
	EOR #$01
SCCRLABEL1:          
	PLY
	EOR $13F9
	BNE SCCRNOCONTACT
	JSL $03B69F
	LDA $13E9
	SEC
	SBC #$02
	STA $00
	LDA $13EA
	SBC #$00
	STA $08
	LDA #$14
	STA $02
	LDA $13EB
	STA $01
	LDA $13EC
	STA $09
	LDA #$10
	STA $03
SCCRFINNISH:         
	JSL $03B72B
	BCC SCCRNOCONTACT
	LDA #$01
	RTS
SCCRNOCONTACT:
	LDA #$00
	RTS
;==================================================================
;Fireball HP Routine
;==================================================================
FireballHP:
	PHX			; push x and y
	PHY
	LDY #$00		; reset fireball index
TRYFIRE:
	LDA $1727,y   		; Store fireball locations
	SEC                       
	SBC #$02                
	STA $00                   
	LDA $173B,y   
	SBC #$00                
	STA $08                   
	LDA #$0C                
	STA $02                   
	LDA $171D,y   
	SEC                       
	SBC #$04                
	STA $01                
	LDA $1731,y   
	SBC #$00                
	STA $09                   
	LDA #$13                ; clipping related???
	STA $03                   
	PHY			; push fireball index
	JSL $03B69F 		; get sprite clipping
	JSL $03B72B 		; contact
	BCS FOUNDFIRE		; end if found fireball
	PLY			; pull fireball index
	CPY #$00		; try again for second fireball
	BNE NOFIRE		; if second failed end
	INY
	BRA TRYFIRE
FOUNDFIRE:
	PLY			; pull fireball index
				; Y has fireball index--ldy RAM, stz $1713,y for instant disp, store 1 for hit something
	PLX			; don't recover Y
	PLX			; pull X
	LDA #$01
	RTS
NOFIRE:
	PLY			; pull x and y
	PLX
	LDA #$00		; beq to branch if fireball not found
	RTS
;==================================================================
;Limit X Speed Subroutine	
;==================================================================
LimitXSpd:
	LDY $7B
	LDA $76
	BNE RightD ; Branch to right.

	CPY.b #$FF-!Speed
	BMI DoFix2
	RTS
DoFix2:
	LDA.b #$FF-!Speed
	STA $7B
	RTS
RightD:
	CPY #!Speed
	BPL Fix ; If going beyond limit, fix speed.
	RTS
Fix:
	LDA #!Speed
	STA $7B
	RTS
;==================================================================
;Change Mario Subroutine	
;==================================================================
ChangePals:
	LDA #$86
	STA $2121
	LDA #!High
	STA $2122
	LDA #!Low
	STA $2122
	LDA #$8E
	STA $2121
	LDA #!High
	STA $2122
	LDA #!Low
	STA $2122
	LDA #$89
	STA $2121
	LDA #!High
	STA $2122
	LDA #!Low
	STA $2122
	LDA #$8C
	STA $2121
	LDA #!High2
	STA $2122
	LDA #!Low2
	STA $2122
	RTS	
;==================================================================
;Hurt/Poison Mario Subroutine
;==================================================================
Poision:
	JSL $01A7DC ; 
	BCC NoContact2
	LDA !Flash,x
	BNE NoContact2
	LDA !InvTimer
	CMP #$02
	BCS NoContact3	; If invisibility is going on, no contact.
	LDA !Poision,x
	BNE OnlyHurt ; Return if already poisioned.
NoContact3:
	JSR SUB_VERT_POS
	LDA $0E
	CMP #$E6
	BPL DoPos
NoContact2:
	RTS
DoPos:
	LDA #!PoTimer
	STA !Poision,x
OnlyHurt:
	JSL $00F5B7
	RTS
;==================================================================
;Stun Ground Subroutine
;==================================================================
Strong:
	LDY #!StunTimerA
DoStun:
	LDA !InvTimer+8
	BEQ NoStun
	LDA $1588,x
	AND #$04
	BNE CheckFlag
	LDA #$01
	STA !InvTimer+3	; Flagg'd if in air.
	RTS
CheckFlag: 
	LDA !InvTimer+3
	BEQ NoStun
	LDA $77
	AND #$04
	BEQ + ; Store Y to $18BD if on the ground as well.
	STY $18BD
+
	STY $1887
	LDA #$09
	STA $1DFC
	LDA #$00
	STA !InvTimer+3
NoStun:
	RTS ; 
Strong2:
	LDY #!StunTimerB
	BRA DoStun
;==================================================================
;Generate Sprite Routine
;==================================================================

RandSpr:
	LDA $13
	AND #$7F
	BNE Fail
	LDA $15A0,x
	ORA $186C,x
	ORA $15D0,x
	BNE Fail
	JSL $02A9DE
	BMI Fail
	LDA #$09
	STA $1DFC
	LDA #!INITSTAT
	STA $14C8,y
	PHX
	LDA #$02
	JSL RANDOM
	TAX
	LDA SPRITE_TO_GEN,x
	PLX
	STA $009E,y
	LDA $E4,x
	STA $00E4,y
	LDA $14E0,x
	STA $14E0,y
	LDA $D8,x	
	SEC
	SBC #$01
	STA $00D8,y
	LDA $14D4,x
	SBC #$00
	STA $14D4,y
	PHX
	TYX
	JSL $07F7D2
	PLX
Fail:	RTS

RANDOM:
	PHX
	PHP
	SEP #$30
	PHA
	JSL $01ACF9
	PLX
	CPX #$FF
	BNE NORMALRT
	LDA $148B
	BRA ENDRANDOM
NORMALRT:
	INX
	LDA $148B
	STA $4202
	STX $4203
	NOP #4
	LDA $4217	
ENDRANDOM:
	PLP
	PLX
	RTL
;======
GenNSpr:
	LDA $14
	AND #$7F
	ORA $9D
	BNE RETURN2
	JSL $02A9DE
	BMI RETURN2
	PHX
	TYX
	LDA #$01
	STA $14C8,x
	LDA #!NormalSprite
	STA $9E,x
	JSL $07F7D2
	TXY
	PLX
	LDA $E4,x
	STA $00E4,y
	LDA $14E0,x
	STA $14E0,y
	LDA $D8,x
	STA $00D8,y
	LDA $14D4,x
	STA $14D4,y
RETURN2:
	RTS
;======
GenCSpr:
	LDA $14
	AND #$7F
	ORA $9D
	BNE RETURN
	JSL $02A9DE
	BMI RETURN
	PHX
	TYX
	LDA #$01
	STA $14C8,x
	LDA #!CustomSprite
	STA $7FAB9E,x
	JSL $07F7D2
	JSL $0187A7
	LDA #$08
	STA $7FAB10,x
	TXY
	PLX
	LDA $E4,x
	STA $00E4,y
	LDA $14E0,x
	STA $14E0,y
	LDA $D8,x
	STA $00D8,y
	LDA $14D4,x
	STA $14D4,y
RETURN:
	RTS
;==================================================================
;Random Movement Routine
;==================================================================
RandMove:
	LDA $13
	AND #$7F ; CHANGE THIS IF YOU WANT. RAND MOVEMENT DURATION. ABOVE = MORE, BELOW 7F = LESS.
	BNE +
	LDA $157C,x
	EOR #$01
	STA $157C,x
+
	RTS
;==================================================================
;EndLevel/Teleport/Whatever Routine
;==================================================================
DoExit:
	LDA #!ExitType
	CMP #$00	; 00 is needed here.
	BEQ NormalExit ; .. so is a pointer.
	CMP #$01
	BEQ SecretExit
	CMP #$02
	BEQ Teleport
	CMP #$03
	BEQ OWFade
NoExit:
	RTS
SecretExit:
	LDA #$01
	STA $141C
	LDA #$FF	
	STA $1493
	RTS
NormalExit:
	LDA #$FF
	STA $1493
	DEC $13C6 
	LDA #!ExitSFX
	STA $1DFB ; Store sound and exit level.
	RTS
Teleport:
	LDA #$06	; Really simple ..
	STA $71		; .. and my first ASM code.
	STZ $89
	STZ $88		; Teleport.
	RTS
OWFade:
	LDA #!OWExit
	TAX 
	LDA $0DD5 				
	BEQ Skip 		
	BPL NoFade
Skip: 
	TXA 
	STA $13CE
	STA $0DD5
	INC $1DE9	
	LDA #$0B
	STA $0100
NoFade: 
	RTS	
;==================================================================
;Message Subroutines
;==================================================================
Msg:
	LDA $151C,x
	AND #$01	; Bit one controls message one.
	BNE NoSwitch
	LDA $13	
	AND #$3F
	BNE NoSwitch
	LDA #!Msg1L
	STA $13BF ; .. I don't know if this has any side effects..
	LDA #!Msg1T ; Load message one type..
	STA $1426
	LDA $151C,x
	ORA #$01
	STA $151C,x
NoSwitch:
	RTS
Msg2:
	LDA $151C,x
	AND #$02	; Bit one controls message one.
	BNE NoSwitch2
	LDA $13	
	AND #$3F
	BNE NoSwitch2
	LDA #!Msg2L
	STA $13BF ; .. I don't know if this has any side effects..
	LDA #!Msg2T ; Load message one type..
	STA $1426
	LDA $151C,x
	ORA #$02
	STA $151C,x
NoSwitch2:
	RTS
;==================================================================
;Brightness/Darkness Subroutine
;==================================================================
Dark2:
	LDA #!Dark2
	BRA StoreB
Dark:
	LDA #!Dark
	BRA StoreB
Bright:
	LDA #$0F ; Restore normal brightness..
	STA $0DAE
	RTS
StoreB:
	STA $0DAE
	RTS	
;==================================================================
;"Sprite" Hurt Subroutine (Shell, Throw Block etc.)
;==================================================================
ThrowHurt:
	LDA !Flash,x
	BNE NoProc 		; Don't process if sprite is already flashing.
	LDA !InvTimer
	CMP #$02
	BCS NoProc 		; .. Or if the invisibility timer of the sprite is on.
	JSL $01A7DC
	BCC NotTouching
	LDA $0E
	CMP #$E6
	BPL HurtHurt
	LDA $140D
	BEQ HurtHurt
	LDA #!NoSpin
	BEQ HurtHurt
	LDA #$02
	STA $1DF9
	JSL $01AA33
	JSL $01AB99
	LDA #!Bouncy
	BEQ +
	JSR SUB_HORZ_POS
	TYA
	EOR #$01
	TAY
	LDA BounceMarioX,y
	STA $7B
+	LDA #$BC
	STA $7D
	RTS
HurtHurt:
	JSL $00F5B7		; Hurt Mario regardless of whatever. It's what ThrowHurt is supposed to do.
	RTS
NotTouching:
	LDY #$0B
Loop:
	LDA $14C8,y
	CMP #$09
	BEQ Process
	CMP #$0A
	BEQ Process
LoopSprSpr:
	DEY
	BPL Loop
NoProc:
	RTS
Process:
	PHX
	TYX
	JSL $03B6E5
	PLX
	JSL $03B69F
	JSL $03B72B
	BCC LoopSprSpr

	PHX
	TYX
	STZ $14C8,x

	LDA $E4,x
	STA $9A
	LDA $14E0,x
	STA $9B
	LDA $D8,x
	STA $98
	LDA $14D4,x
	STA $99

	PHB
	LDA #$02
	PHA
	PLB
	LDA #$FF
	JSL $028663
	PLB
	PLX
	DEC $1528,x
	LDA #!FTimer
	STA !Flash,x
	RTS
;==================================================================
;Jump Hurt/Death Subroutine
;==================================================================

SprWins:		; When the sprite touches Mario.
	JSL $00F5B7	; Hurt him.
	RTS
JumpHurt:
	LDA !Flash,x
	BNE NoContact	; If the sprite is flashing, there's no contact.
	JSL $01A7DC
	BCC NoContact	; No contact if sprite touches Mario.
	LDA !InvTimer
	CMP #$02
	BCS NoContactX	; If invisibility is going on, no contact.

	JSR SUB_VERT_POS
	LDA $0E
	CMP #$E6
	BPL SprWins
	LDA $7D		;\ If Y speed negative (rising) ..
	BMI NoContact	;/ ..skip.

	JSL $01AA33
	JSL $01AB99	; "Stomp" graphic.
	LDA #$02
	STA $1DF9
	LDA #!Bouncy
	BEQ +
	JSR SUB_HORZ_POS
	TYA
	EOR #$01
	TAY
	LDA BounceMarioX,y
	STA $7B
+
	LDA #$BC
	STA $7D
	
	LDA $140D
	BEQ NoSpinning ; If spinning ..

	LDA #!NoSpin
	CMP #$01	
	BEQ NoContact
	
NoSpinning:
	LDA #$20
	STA $1DF9
	LDA #!FTimer
	STA !Flash,x	; Store flash timer.
	DEC $1528,x
NoContact:
	RTS
NoContactX:
	JSR SUB_VERT_POS	;\
	LDA $0E			; | Sprite can hurt Mario, Mario can't.
	CMP #$E6		;/
	BPL SprWins
	;LDA $7D		;\ If Y speed negative (rising) ..
	;BMI NoContact		;/ ..skip.
	;RTS
KillSprite:
	LDA #!DeathType
	ASL A
	PHX
	TAX
	JMP (DeathPtr,x)
	RTS
DeathPtr:
	dw StarKill ; 00
	dw Dispp ; 01
	dw Bomb ; 02
StarKill:
	PLX
	STZ !Flash,x	; No flashing.
	LDA #$02
	STA $14C8,x
	LDA #$D0
	STA $AA,x
	LDA #$08
	STA $B6,x
	JSL $01802A
	JMP DoExit
Dispp:
	PLX
	LDA #$04
	STA $14C8,x	; Disappear in a puff of smoke.
	JSL $03A6C8
	JMP DoExit
Bomb:
	PLX		;\ Restore sprite index.
	LDA #$0D	;/
	STA $9E,x	;\ Sprite = Bob-omb.
	LDA #$08	;/
	STA $14C8,x	;\ Set status for new sprite.
	JSL $07F7D2	;/ Reset sprite tables (sprite becomes bob-omb)..
	LDA #$01	;\ .. and flag explosion status.
	STA $1534,x	;/
	LDA #$40	;\ Time for explosion.
	STA $1540,x
	LDA #$09	;\
	STA $1DFC	;/ Sound effect.
	LDA #$1B
	STA $167A,x
	JMP DoExit
;==================================================================
;On/Off and P-Switch Subroutines
;==================================================================
OnOff:
	LDA #$01
	STA $14AF	; Turn the ON/OFF switch to OFF.
	RTS
PSwitch:
	LDA #!PTimer	; Load P-Switch Timer ..
	STA $14AD	; Duration into $14AD.
	RTS
;==================================================================
;Jump A/B/C Subroutines	
;==================================================================
JumpA:
	JSR IncJTimer
	CMP #$04
	BCC +
	LDA #$B4
	BRA ApplySpdY
+
	RTS
JumpB:
	JSR IncJTimer		; Increase The Jump Timer every second.	
	CMP #$03		; .. If it's 02..
	BCC +		
	LDA #$B7
	BRA ApplySpdY
+
	RTS
JumpC:
	JSR IncJTimer		; Increase The Jump Timer every second.	
	CMP #$02		; .. If it's 01..
	BCC +		
	LDA #$BA
	BRA ApplySpdY
+
	RTS
ApplySpdY:
	STA $AA,x		; Make the sprite jump.
	JSL $01802A		; Update new Y speed.
	LDA #$00
	STA !InvTimer+7
	LDA $157C,x
	EOR #$01
	STA $157C,x
	RTS	
ClearJump:
	LDA #$00
	STA !InvTimer+6
	RTS
;==================================================================
; Spit Fireball Subroutine
;==================================================================
SpitFire:
	LDA $13
	AND #$3F
	BNE +
	JSR DoSpit
+
	RTS
DoSpit:
	LDY #$07
-
	LDA $170B,y
	BEQ +
	DEY
	BPL -
	RTS
+
	LDA #$02
	STA $170B,y
	
	PHY
	LDY $157C,x
	LDA $E4,x
	CLC
	ADC XFire,y 		; Also XDISP.
	PLY
	STA $171F,y
	PHY
	LDY $157C,x
	LDA $14E0,x
	ADC XDISPHI,y
	PLY
	STA $1733,y

	LDA $D8,x
	CLC
	ADC #$05
	STA $1715,y
	LDA $14D4,x
	ADC #$00
	STA $1729,y
	PHX
	
	LDA #$04
	JSL RANDOM
	TAX
	LDA YFire,x
	STA $173D,y
	PLX
	LDA $157C,x
	PHX
	TAX
	LDA XFire,x
	STA $1747,y
	PLX
	JSR SUB_HORZ_POS
	TYA
	STA $157C,x
	LDA #$06
	STA $1DFC
	RTS

YFire: db $FE,$04,$F9,$FC,$05
XFire: db $10,$F0
XDISPHI: db $00,$FF
;==================================================================
;Hammer A/B Subroutines	
;==================================================================
HammerA:
	LDA $13
	CMP #$26
	BEQ ThrowHammer
	CMP #$5D
	BEQ ThrowHammer
	CMP #$6D
	BEQ ThrowHammer
	CMP #$BA
	BEQ ThrowHammer
	CMP #$E2
	BEQ ThrowHammer
	RTS
HammerB:
	LDA $13
	CMP #$16
	BEQ ThrowHammer
	CMP #$31
	BEQ ThrowHammer
	CMP #$77
	BEQ ThrowHammer
	CMP #$AA
	BEQ ThrowHammer
	CMP #$CC
	BEQ ThrowHammer
	CMP #$EA
	BEQ ThrowHammer
	RTS
;==================================================================
;Roar Subroutine
;==================================================================
Roar:
	JSR SUB_HORZ_POS
	TYA	
	STA $157C,x
	JSR HammerB
	LDA $13
	AND #$07
	BNE .R
	JSR ThrowHammer
.R
	LDA #$25
	STA $1DFC
	RTS
ThrowHammer:
	LDY #$07	; # of times to go through loop..
-
	LDA $170B,y	; Check to see ..
	BEQ +		; .. If a slot is found.
	DEY
	BPL -		; Otherwise, loop.
	RTS
+
	LDA #$04
	STA $170B,y ; Sprite = Hammer.
	JMP ThrowEx	
;==================================================================
;Bone A/B Subroutines	
;==================================================================
BoneA:
	LDA $13
	CMP #$35
	BEQ ThrowBone
	CMP #$68
	BEQ ThrowBone
	CMP #$AC
	BEQ ThrowBone
	RTS
BoneB:
	LDA $13
	CMP #$25
	BEQ ThrowBone
	CMP #$55
	BEQ ThrowBone
	CMP #$97
	BEQ ThrowBone
	CMP #$CF
	BEQ ThrowBone
	RTS
ThrowBone:
	LDY #$07	; # of times to go through loop..
-
	LDA $170B,y	; Check to see ..
	BEQ +		; .. If a slot is found.
	DEY
	BPL -		; Otherwise, loop.
	RTS
+
	LDA #$06	; ExSprite = bone.
	STA $170B,y
ThrowEx:
	PHY
	LDA $E4,x
	LDY $76
	CLC
	ADC XGenPos,y
	PLY
	STA $171F,y	; Generate positions. (X Low, Y Low, X High, Y High).
	;ADC #$00
	LDA $14E0,x
	STA $1733,y
	LDA $D8,x
	STA $1715,y
	LDA $14D4,x
	STA $1729,y
	LDA #$C7
	STA $173D,y ; Y speed.
	PHY
	LDY $157C,x
	LDA ExSprSpd,y
	PLY
	STA $1747,y
	LDA #$FF
	STA $176F,y
	LDA #!ExSound
	STA $1DF9
+	RTS

ExSprSpd: db $25,$DA
XGenPos: db $02,$00-02
;==================================================================
;Follow Subroutine	
;==================================================================
Follow:
	JSR Proximity		;\
	BNE +			;/ NOTE: This is done to avoid glitchfest.
	JSR SUB_HORZ_POS	; Get sprite's direction relative to Mario's.
	TYA
	STA $157C,x		; And store it.
+
	RTS
Proximity:		; If sprite is TOO close to Mario, return.
	LDA $14E0,x
	XBA
	LDA $E4,x
	REP #$20 ; Get sprite's X position.
	SEC
	SBC $94	; Subtract Mario's to get the difference range.
	SEP #$20
	PHA
	JSR SUB_HORZ_POS ; Determine sprite range.
	PLA
	EOR InvertAbility,y ; Apply inversion based on direction.
	CMP #$09 ; Range.
	BCS OutofRange
	LDA #$01
	RTS
OutofRange:
	LDA $E4,x
	CMP $94
	BNE RangeOut
	LDA #$01
	RTS
RangeOut:
	LDA #$00
ThisIsTheBiggestEverNameThatYouWillHaveEverSeenInASpritesASMFileInsertedThroughRomisSpriteToolAlsoThisIsTheBiggestEpicLolRofl:
	RTS
InvertAbility:
	db $FF,$00
;==================================================================
;Moving/Rising/Dropping Subroutine (Move) (Rise) (Drop)	
;==================================================================
Move:
	LDA !InvTimer+10 ; COMPLETELY SKIP IF ACCELERATING.
	ORA $163E,x
	BNE ThisIsTheBiggestEverNameThatYouWillHaveEverSeenInASpritesASMFileInsertedThroughRomisSpriteToolAlsoThisIsTheBiggestEpicLolRofl

	PHY
	LDY $1528,x 	; Get sprite health into Y.
	LDA $157C,x 	; Get the sprite's direction ..
	BEQ Right
	LDA Tbl1R,y	;\
	EOR #$FF	; |
	INC A		; | Sprite left speed = $1626.
	STA $1626;,y
	LDA $1626;,y
	BRA SetSpeedC
Right:
	LDA Tbl1R,y 	; Get the speeds value based on health.
SetSpeedC:
	STA $B6,x 	; Store the sprite X speed.
	LDA !InvTimer+6
	BNE UpdatePos ; The Y speed CANNOT be updated if jumping.
		      ; Jumping will control that instead.

	LDA !YMove,x
	CMP #$02
	BEQ DropSpr
	CMP #$01
	BEQ CheckYPos

	LDA #!Gravity
	STA $AA,x ;Gravity update if zero.
	BRA UpdatePos

CheckYPos:
	LDA $14D4,x
	XBA
	LDA $D8,x
	REP #$20 ; Check the Y position.
	CMP.w #!RiseLoc
	SEP #$20
	BCS .Rise ; .. this should actually be BCC?
	STZ $AA,x
	BRA UpdatePos
.Rise
	LDA #!RiseSpeed
	STA $AA,x
	STZ $B6,x
	STZ !YMove,x
UpdatePos:
	JSL $01802A 	; Update positions based on speed.
PullNoUpdate:
	PLY
	RTS
Rise:
	LDA #$01
	STA !YMove,x
	RTS
Drop:
	LDA $1588,x
	AND #$04
	BNE .Stop
	LDA #$02
	STA !YMove,x
	RTS
.Stop
	PHY
	STZ !YMove,x
	BRA PullNoUpdate
DropSpr:
	LDA #!DropSpeed
	STA $AA,x
	STZ $B6,x
	BRA UpdatePos	
;==================================================================
;Wavy Motion Subroutine.	
;==================================================================

WavyMotion:
	PHY ; Push Y in case something messes it up.
	LDA $14 ; Get the sprite frame counter ..
	LSR A ; 7F
	LSR A ; 3F ; Tip: LSR #3.
	LSR A ; 1F
	AND #$07 ; Loop through these bytes during H-Blank. (WHAT?)
	TAY ; Into Y.
	LDA WavySpd,y ; Load Y speeds ..
	STA $AA,x
	JSL $01801A ; Update, with no gravity.
	PLY ; Pull Y.
	RTS

WavySpd: db $00,$F8,$F2,$F8,$00,$08,$0E,$08
;==================================================================
;InvTimer Increase Routine
;==================================================================
Inv:
	LDA $13
	AND #$3F
	BNE +
	LDA !InvTimer
	INC A
	STA !InvTimer
+
	LDA !InvTimer
	CMP #$04
	BCC ClearA ; If the timer has reached 5 ..
ClearInv:
	LDA #$00
	STA !InvTimer ; Clear it.
ClearA:
	RTS
;==================================================================
;Timer Increase Routine
;==================================================================
IncJTimer:	
	LDA #$01
	STA !InvTimer+6
	LDA $13			; Every 3F Frames ..
	AND #$3F
	BNE +
	LDA !InvTimer+7
	INC A
	STA !InvTimer+7
+
	LDA !InvTimer+7
	RTS			; Load it, to save bytes.
;==================================================================
;Draw Tiles To Status Bar.	
;==================================================================
DrawTilemaps:
	LDX #!NameSize	
DrawLoop:
	LDA NameTable,x 	; Load the tile data indexed by the size.
	STA !Position,x	 	; .. and store to appriopriate tile.
	DEX
	BPL DrawLoop 		; So far, it draws "AAAA"
	LDA #$26
	STA !Position+!NameSize+1 ; Now, "AAAAx"
	RTS ; We draw the health routine later, after pulling X.
	; ORLY
;==================================================================
;Hex/Dec Subroutine.
;==================================================================
HexDec:
	LDX #$00
HexLoop:
	CMP #$0A ; While (A == $0A) {
	BCC NoConvo ; }
	SBC #$0A	; Else { DrawLine.(A - 10); Fail C++ ftw.
	INX ; (X + 1) }
	BRA HexLoop ; Cout << ("C++ Fail ftw");
NoConvo: ; Oh wait where's the cin command?!
	RTS
;==================================================================
;Animation Change
;==================================================================
Animate3:
	LDA #$02
	BRA SetAni
Animate2:
	LDA #$01
	BRA SetAni
Animate:
	LDA #$00
SetAni:
	STA !InvTimer+9
	RTS
;======================================================================
;Graphics Routine
;======================================================================

PROPERTIES:          
	db $40,$00
TILEMAP:
	db !TopLeft,!TopRight,!BottomLeft,!BottomRight,!WalkTopLeft,!WalkTopRight,!WalkBottomLeft,!WalkBottomRight
        db !TopLeft,!TopRight,!BottomLeft,!BottomRight,!WalkTopLeft,!WalkTopRight,!WalkBottomLeft,!WalkBottomRight
TILEMAP2:
	db !TopLeft2,!TopRight2,!BottomLeft2,!BottomRight2,!WalkTopLeft2,!WalkTopRight2,!WalkBottomLeft2,!WalkBottomRight2
        db !TopLeft2,!TopRight2,!BottomLeft2,!BottomRight2,!WalkTopLeft2,!WalkTopRight2,!WalkBottomLeft2,!WalkBottomRight2
TILEMAP3:
	db !TopLeft3,!TopRight3,!BottomLeft3,!BottomRight3,!WalkTopLeft3,!WalkTopRight3,!WalkBottomLeft3,!WalkBottomRight3
        db !TopLeft3,!TopRight3,!BottomLeft3,!BottomRight3,!WalkTopLeft3,!WalkTopRight3,!WalkBottomLeft3,!WalkBottomRight3
VERT_DISP:          
	db $F0,$F0,$00,$00,$F0,$F0,$00,$00
        db $F0,$F0,$00,$00,$F0,$F0,$00,$00         
HORIZ_DISP:  
	db $F8,$08,$F8,$08,$F8,$08,$F8,$08
	db $08,$F8,$08,$F8,$08,$F8,$08,$F8
Graphics:
	LDA !InvTimer
	CMP #$02
	BCS SkipGFX ; Completely skip if invisible.
	BRA Positive
SkipGFX:
	JMP SkipGFX2
Positive:
	JSR GET_DRAW_INFO
	LDA $1602,x
	STA $03 ; Animation frames -> $03.
	
	LDA $157C,x
	STA $02 ; Direction -> $02.
	BNE NoAdd
	LDA $03
	CLC
	ADC #$08 ; Add displacement if going left.
	STA $03
NoAdd:
	PHX ; Push sprite index.
	LDX #$03 ; Get # of times to go through loop.
Loop2:
	PHX 	; Push it for later..
	TXA
	ORA $03
FaceLeft:
	TAX
	LDA $00
	CLC
	ADC HORIZ_DISP,x ; Apply X displacement ..
	STA $0300,y

	LDA $01
	CLC
	ADC VERT_DISP,x ; .. and Y displacement.
	STA $0301,y

	LDA !InvTimer+9		;\
	ASL A			; |
	PHX			; | Animations involve a pointer.
	TAX			; |
	JMP (PtrPtr,x)		;/
;NOTE - Pointer is used to easily display frames (for future updates)
PtrPtr:
	dw AnimationFrameA 	; 00 ; Normal animation frames (A)
	dw AnimationFrameB 	; 01 ; Normal animation frames (B)
	dw AnimationFrameC 	; 02 ; Normal animation frames (C)
	;dw AnimationExFrame 	; 03 ; Throwing bone/hammer frame

AnimationFrameA:
	PLX
	LDA TILEMAP,x
	BRA DoGfx
AnimationFrameB:
	PLX
	LDA TILEMAP2,x
	BRA DoGfx
AnimationFrameC:
	PLX
	LDA TILEMAP3,x
	BRA DoGfx
DoGfx:
	STA $0302,y

	LDX $02 ; X is already preserved, so no need to push it again.
	LDA PROPERTIES,x
	LDX $15E9
	ORA $15F6,x
	ORA $64 ; Add in property byte.
	STA $0303,y

	LDA #!DoFlash ; Skip flashing if we're not allowed.
	CMP #$01
	BNE NoFlashing

	LDA !Flash,x
	BEQ NoFlashing ; Sprite flashes when jumped on.
	LDA $14
	AND #$07 ; 7 frames ..
	TAX ; -> X.
	LDA $0303,y
	AND #$F1
	ORA FlashTable,x
	STA $0303,y
NoFlashing:

	PLX ; Pull #$03.

	INY
	INY
	INY
	INY
	DEX ; Decrease it for every 16x16 tile drawn.
	BPL Loop2

	PLX
	LDY #$02
	LDA #$03
	JSL $01B7B3
SkipGFX2:	
	RTS
;==================================================================
;Borrowed Routines.
;==================================================================

DrawSmoke:          LDY #$03                ; \ find a free slot to display effect
FINDFREE:           LDA $17C0,y             ;  |
                    BEQ FOUNDONE            ;  |
                    DEY                     ;  |
                    BPL FINDFREE            ;  |
                    RTS                     ; / return if no slots open

FOUNDONE:           LDA #$01                ; \ set effect graphic to smoke graphic
                    STA $17C0,y             ; /
                    LDA #$1B                ; \ set time to show smoke
                    STA $17CC,y             ; /
                    LDA $D8,x               ; \ smoke y position = generator y position
                    STA $17C4,y             ; /
                    LDA $E4,x               ; \ load generator x position and store it for later
                    STA $17C8,y             ; /
                    RTS


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; $B817 - horizontal mario/sprite check - shared
; Y = 1 if mario left of sprite??
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

                    ;org $03B817        

SUB_HORZ_POS:        LDY #$00                ;A:25D0 X:0006 Y:0001 D:0000 DB:03 S:01ED P:eNvMXdizCHC:1020 VC:097 00 FL:31642
                    LDA $94                 ;A:25D0 X:0006 Y:0000 D:0000 DB:03 S:01ED P:envMXdiZCHC:1036 VC:097 00 FL:31642
                    SEC                     ;A:25F0 X:0006 Y:0000 D:0000 DB:03 S:01ED P:eNvMXdizCHC:1060 VC:097 00 FL:31642
                    SBC $E4,x               ;A:25F0 X:0006 Y:0000 D:0000 DB:03 S:01ED P:eNvMXdizCHC:1074 VC:097 00 FL:31642
                    STA $0F                 ;A:25F4 X:0006 Y:0000 D:0000 DB:03 S:01ED P:eNvMXdizcHC:1104 VC:097 00 FL:31642
                    LDA $95                 ;A:25F4 X:0006 Y:0000 D:0000 DB:03 S:01ED P:eNvMXdizcHC:1128 VC:097 00 FL:31642
                    SBC $14E0,x             ;A:2500 X:0006 Y:0000 D:0000 DB:03 S:01ED P:envMXdiZcHC:1152 VC:097 00 FL:31642
                    BPL LABEL16             ;A:25FF X:0006 Y:0000 D:0000 DB:03 S:01ED P:eNvMXdizcHC:1184 VC:097 00 FL:31642
                    INY                     ;A:25FF X:0006 Y:0000 D:0000 DB:03 S:01ED P:eNvMXdizcHC:1200 VC:097 00 FL:31642
LABEL16:             RTS                     ;A:25FF X:0006 Y:0001 D:0000 DB:03 S:01ED P:envMXdizcHC:1214 VC:097 00 FL:31642
                    



;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; GET_DRAW_INFO
; This is a helper for the graphics routine.  It sets off screen flags, and sets up
; variables.  It will return with the following:
;
;       Y = index to sprite OAM ($300)
;       $00 = sprite x position relative to screen boarder
;       $01 = sprite y position relative to screen boarder  
;
; It is adapted from the subroutine at $03B760
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

SPR_T1:              db $0C,$1C
SPR_T2:             db $01,$02

GET_DRAW_INFO:       STZ $186C,x             ; reset sprite offscreen flag, vertical
                    STZ $15A0,x             ; reset sprite offscreen flag, horizontal
                    LDA $E4,x               ; \
                    CMP $1A                 ;  | set horizontal offscreen if necessary
                    LDA $14E0,x             ;  |
                    SBC $1B                 ;  |
                    BEQ ON_SCREEN_X         ;  |
                    INC $15A0,x             ; /

ON_SCREEN_X:         LDA $14E0,x             ; \
                    XBA                     ;  |
                    LDA $E4,x               ;  |
                    REP #$20                ;  |
                    SEC                     ;  |
                    SBC $1A                 ;  | mark sprite invalid if far enough off screen
                    CLC                     ;  |
                    ADC #$0040            ;  |
                    CMP #$0180            ;  |
                    SEP #$20                ;  |
                    ROL A                   ;  |
                    AND #$01                ;  |
                    STA $15C4,x             ; / 
                    BNE INVALID             ; 
                    
                    LDY #$00                ; \ set up loop:
                    LDA $1662,x             ;  | 
                    AND #$20                ;  | if not smushed (1662 & 0x20), go through loop twice
                    BEQ ON_SCREEN_LOOP      ;  | else, go through loop once
                    INY                     ; / 
ON_SCREEN_LOOP:      LDA $D8,x               ; \ 
                    CLC                     ;  | set vertical offscreen if necessary
                    ADC SPR_T1,y            ;  |
                    PHP                     ;  |
                    CMP $1C                 ;  | (vert screen boundry)
                    ROL $00                 ;  |
                    PLP                     ;  |
                    LDA $14D4,x             ;  | 
                    ADC #$00                ;  |
                    LSR $00                 ;  |
                    SBC $1D                 ;  |
                    BEQ ON_SCREEN_Y         ;  |
                    LDA $186C,x             ;  | (vert offscreen)
                    ORA SPR_T2,y            ;  |
                    STA $186C,x             ;  |
ON_SCREEN_Y:         DEY                     ;  |
                    BPL ON_SCREEN_LOOP      ; /

                    LDY $15EA,x             ; get offset to sprite OAM
                    LDA $E4,x               ; \ 
                    SEC                     ;  | 
                    SBC $1A                 ;  | $00 = sprite x position relative to screen boarder
                    STA $00                 ; / 
                    LDA $D8,x               ; \ 
                    SEC                     ;  | 
                    SBC $1C                 ;  | $01 = sprite y position relative to screen boarder
                    STA $01                 ; / 
                    RTS                     ; return

INVALID:             PLA                     ; \ return from *main gfx routine* subroutine...
                    PLA                     ;  |    ...(not just this subroutine)
                    RTS                     ; /


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; SUB_OFF_SCREEN
; This subroutine deals with sprites that have moved off screen
; It is adapted from the subroutine at $01AC0D
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                    
SPR_T12:             db $40,$B0
SPR_T13:             db $01,$FF
SPR_T14:             db $30,$C0,$A0,$C0,$A0,$F0,$60,$90		;bank 1 sizes
		            db $30,$C0,$A0,$80,$A0,$40,$60,$B0		;bank 3 sizes
SPR_T15:             db $01,$FF,$01,$FF,$01,$FF,$01,$FF		;bank 1 sizes
					db $01,$FF,$01,$FF,$01,$00,$01,$FF		;bank 3 sizes

SUB_OFF_SCREEN_X1:   LDA #$02                ; \ entry point of routine determines value of $03
                    BRA STORE_03            ;  | (table entry to use on horizontal levels)
SUB_OFF_SCREEN_X2:   LDA #$04                ;  | 
                    BRA STORE_03            ;  |
SUB_OFF_SCREEN_X3:   LDA #$06                ;  |
                    BRA STORE_03            ;  |
SUB_OFF_SCREEN_X4:   LDA #$08                ;  |
                    BRA STORE_03            ;  |
SUB_OFF_SCREEN_X5:   LDA #$0A                ;  |
                    BRA STORE_03            ;  |
SUB_OFF_SCREEN_X6:   LDA #$0C                ;  |
                    BRA STORE_03            ;  | OMG YOU FOUND THIS HIDDEN z0mg place!111 you win a cookie!
SUB_OFF_SCREEN_X7:   LDA #$0E                ;  |
STORE_03:			STA $03					;  |            
					BRA START_SUB			;  |
SUB_OFF_SCREEN_X0:   STZ $03					; /

START_SUB:           JSR SUB_IS_OFF_SCREEN   ; \ if sprite is not off screen, return
                    BEQ RETURN_35           ; /
                    LDA $5B                 ; \  goto VERTICAL_LEVEL if vertical level
                    AND #$01                ; |
                    BNE VERTICAL_LEVEL      ; /     
                    LDA $D8,x               ; \
                    CLC                     ; | 
                    ADC #$50                ; | if the sprite has gone off the bottom of the level...
                    LDA $14D4,x             ; | (if adding 0x50 to the sprite y position would make the high byte >= 2)
                    ADC #$00                ; | 
                    CMP #$02                ; | 
                    BPL ERASE_SPRITE        ; /    ...erase the sprite
                    LDA $167A,x             ; \ if "process offscreen" flag is set, return
                    AND #$04                ; |
                    BNE RETURN_35           ; /
                    LDA $13                 ;A:8A00 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdiZcHC:0756 VC:176 00 FL:205
                    AND #$01                ;A:8A01 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizcHC:0780 VC:176 00 FL:205
                    ORA $03                 ;A:8A01 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizcHC:0796 VC:176 00 FL:205
                    STA $01                 ;A:8A01 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizcHC:0820 VC:176 00 FL:205
                    TAY                     ;A:8A01 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizcHC:0844 VC:176 00 FL:205
                    LDA $1A                 ;A:8A01 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizcHC:0858 VC:176 00 FL:205
                    CLC                     ;A:8A00 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdiZcHC:0882 VC:176 00 FL:205
                    ADC SPR_T14,y           ;A:8A00 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdiZcHC:0896 VC:176 00 FL:205
                    ROL $00                 ;A:8AC0 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:eNvMXdizcHC:0928 VC:176 00 FL:205
                    CMP $E4,x               ;A:8AC0 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:eNvMXdizCHC:0966 VC:176 00 FL:205
                    PHP                     ;A:8AC0 X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizCHC:0996 VC:176 00 FL:205
                    LDA $1B                 ;A:8AC0 X:0009 Y:0001 D:0000 DB:01 S:01F0 P:envMXdizCHC:1018 VC:176 00 FL:205
                    LSR $00                 ;A:8A00 X:0009 Y:0001 D:0000 DB:01 S:01F0 P:envMXdiZCHC:1042 VC:176 00 FL:205
                    ADC SPR_T15,y           ;A:8A00 X:0009 Y:0001 D:0000 DB:01 S:01F0 P:envMXdizcHC:1080 VC:176 00 FL:205
                    PLP                     ;A:8AFF X:0009 Y:0001 D:0000 DB:01 S:01F0 P:eNvMXdizcHC:1112 VC:176 00 FL:205
                    SBC $14E0,x             ;A:8AFF X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizCHC:1140 VC:176 00 FL:205
                    STA $00                 ;A:8AFF X:0009 Y:0001 D:0000 DB:01 S:01F1 P:eNvMXdizCHC:1172 VC:176 00 FL:205
                    LSR $01                 ;A:8AFF X:0009 Y:0001 D:0000 DB:01 S:01F1 P:eNvMXdizCHC:1196 VC:176 00 FL:205
                    BCC SPR_L31             ;A:8AFF X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdiZCHC:1234 VC:176 00 FL:205
                    EOR #$80                ;A:8AFF X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdiZCHC:1250 VC:176 00 FL:205
                    STA $00                 ;A:8A7F X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizCHC:1266 VC:176 00 FL:205
SPR_L31:             LDA $00                 ;A:8A7F X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizCHC:1290 VC:176 00 FL:205
                    BPL RETURN_35           ;A:8A7F X:0009 Y:0001 D:0000 DB:01 S:01F1 P:envMXdizCHC:1314 VC:176 00 FL:205
ERASE_SPRITE:        LDA $14C8,x             ; \ if sprite status < 8, permanently erase sprite
                    CMP #$08                ; |
                    BCC KILL_SPRITE         ; /    
                    LDY $161A,x             ;A:FF08 X:0007 Y:0001 D:0000 DB:01 S:01F3 P:envMXdiZCHC:1108 VC:059 00 FL:2878
                    CPY #$FF                ;A:FF08 X:0007 Y:0000 D:0000 DB:01 S:01F3 P:envMXdiZCHC:1140 VC:059 00 FL:2878
                    BEQ KILL_SPRITE         ;A:FF08 X:0007 Y:0000 D:0000 DB:01 S:01F3 P:envMXdizcHC:1156 VC:059 00 FL:2878
                    LDA #$00                ;A:FF08 X:0007 Y:0000 D:0000 DB:01 S:01F3 P:envMXdizcHC:1172 VC:059 00 FL:2878
                    STA $1938,y             ;A:FF00 X:0007 Y:0000 D:0000 DB:01 S:01F3 P:envMXdiZcHC:1188 VC:059 00 FL:2878
KILL_SPRITE:         STZ $14C8,x             ; erase sprite
RETURN_35:           RTS                     ; return

VERTICAL_LEVEL:      LDA $167A,x             ; \ if "process offscreen" flag is set, return
                    AND #$04                ; |
                    BNE RETURN_35           ; /
                    LDA $13                 ; \
                    LSR A                   ; | 
                    BCS RETURN_35           ; /
                    LDA $E4,x               ; \ 
                    CMP #$00                ;  | if the sprite has gone off the side of the level...
                    LDA $14E0,x             ;  |
                    SBC #$00                ;  |
                    CMP #$02                ;  |
                    BCS ERASE_SPRITE        ; /  ...erase the sprite
                    LDA $13                 ;A:0000 X:0009 Y:00E4 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:1218 VC:250 00 FL:5379
                    LSR A                   ;A:0016 X:0009 Y:00E4 D:0000 DB:01 S:01F3 P:envMXdizcHC:1242 VC:250 00 FL:5379
                    AND #$01                ;A:000B X:0009 Y:00E4 D:0000 DB:01 S:01F3 P:envMXdizcHC:1256 VC:250 00 FL:5379
                    STA $01                 ;A:0001 X:0009 Y:00E4 D:0000 DB:01 S:01F3 P:envMXdizcHC:1272 VC:250 00 FL:5379
                    TAY                     ;A:0001 X:0009 Y:00E4 D:0000 DB:01 S:01F3 P:envMXdizcHC:1296 VC:250 00 FL:5379
                    LDA $1C                 ;A:001A X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:0052 VC:251 00 FL:5379
                    CLC                     ;A:00BD X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:0076 VC:251 00 FL:5379
                    ADC SPR_T12,y           ;A:00BD X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:0090 VC:251 00 FL:5379
                    ROL $00                 ;A:006D X:0009 Y:0001 D:0000 DB:01 S:01F3 P:enVMXdizCHC:0122 VC:251 00 FL:5379
                    CMP $D8,x               ;A:006D X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNVMXdizcHC:0160 VC:251 00 FL:5379
                    PHP                     ;A:006D X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNVMXdizcHC:0190 VC:251 00 FL:5379
                    LDA $001D             ;A:006D X:0009 Y:0001 D:0000 DB:01 S:01F2 P:eNVMXdizcHC:0212 VC:251 00 FL:5379
                    LSR $00                 ;A:0000 X:0009 Y:0001 D:0000 DB:01 S:01F2 P:enVMXdiZcHC:0244 VC:251 00 FL:5379
                    ADC SPR_T13,y           ;A:0000 X:0009 Y:0001 D:0000 DB:01 S:01F2 P:enVMXdizCHC:0282 VC:251 00 FL:5379
                    PLP                     ;A:0000 X:0009 Y:0001 D:0000 DB:01 S:01F2 P:envMXdiZCHC:0314 VC:251 00 FL:5379
                    SBC $14D4,x             ;A:0000 X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNVMXdizcHC:0342 VC:251 00 FL:5379
                    STA $00                 ;A:00FF X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:0374 VC:251 00 FL:5379
                    LDY $01                 ;A:00FF X:0009 Y:0001 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:0398 VC:251 00 FL:5379
                    BEQ SPR_L38             ;A:00FF X:0009 Y:0001 D:0000 DB:01 S:01F3 P:envMXdizcHC:0422 VC:251 00 FL:5379
                    EOR #$80                ;A:00FF X:0009 Y:0001 D:0000 DB:01 S:01F3 P:envMXdizcHC:0438 VC:251 00 FL:5379
                    STA $00                 ;A:007F X:0009 Y:0001 D:0000 DB:01 S:01F3 P:envMXdizcHC:0454 VC:251 00 FL:5379
SPR_L38:             LDA $00                 ;A:007F X:0009 Y:0001 D:0000 DB:01 S:01F3 P:envMXdizcHC:0478 VC:251 00 FL:5379
                    BPL RETURN_35           ;A:007F X:0009 Y:0001 D:0000 DB:01 S:01F3 P:envMXdizcHC:0502 VC:251 00 FL:5379
                    BMI ERASE_SPRITE        ;A:8AFF X:0002 Y:0000 D:0000 DB:01 S:01F3 P:eNvMXdizcHC:0704 VC:184 00 FL:5490

SUB_IS_OFF_SCREEN:   LDA $15A0,x             ; \ if sprite is on screen, accumulator = 0 
                    ORA $186C,x             ; |  
                    RTS                     ; / return

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; SUB_VERT_POS
; This routine determines if Mario is above or below the sprite.  It sets the Y register
; to the direction such that the sprite would face Mario
; It is ripped from $03B829
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

SUB_VERT_POS:		LDY #$00               ;A:25A1 X:0007 Y:0001 D:0000 DB:03 S:01EA P:envMXdizCHC:0130 VC:085 00 FL:924
					LDA $96                ;A:25A1 X:0007 Y:0000 D:0000 DB:03 S:01EA P:envMXdiZCHC:0146 VC:085 00 FL:924
					SEC                    ;A:2546 X:0007 Y:0000 D:0000 DB:03 S:01EA P:envMXdizCHC:0170 VC:085 00 FL:924
					SBC $D8,x              ;A:2546 X:0007 Y:0000 D:0000 DB:03 S:01EA P:envMXdizCHC:0184 VC:085 00 FL:924
					STA $0F                ;A:25D6 X:0007 Y:0000 D:0000 DB:03 S:01EA P:eNvMXdizcHC:0214 VC:085 00 FL:924
					LDA $97                ;A:25D6 X:0007 Y:0000 D:0000 DB:03 S:01EA P:eNvMXdizcHC:0238 VC:085 00 FL:924
					SBC $14D4,x            ;A:2501 X:0007 Y:0000 D:0000 DB:03 S:01EA P:envMXdizcHC:0262 VC:085 00 FL:924
					BPL SPR_L11            ;A:25FF X:0007 Y:0000 D:0000 DB:03 S:01EA P:eNvMXdizcHC:0294 VC:085 00 FL:924
					INY                    ;A:25FF X:0007 Y:0000 D:0000 DB:03 S:01EA P:eNvMXdizcHC:0310 VC:085 00 FL:924
SPR_L11:				RTS                    ;A:25FF X:0007 Y:0001 D:0000 DB:03 S:01EA P:envMXdizcHC:0324 VC:085 00 FL:924


print bytes
print bytes
print opcodes