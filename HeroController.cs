// HeroController
using GlobalEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
	public delegate void HeroInPosition(bool forceDirect);

	private bool verboseMode;

	public HeroType heroType;

	public float RUN_SPEED;

	public float WALK_SPEED;

	public float UNDERWATER_SPEED;

	public float JUMP_SPEED;

	public float JUMP_SPEED_UNDERWATER;

	public float MIN_JUMP_SPEED;

	public int JUMP_STEPS;

	public int JUMP_STEPS_MIN;

	public int JUMP_TIME;

	public int DOUBLE_JUMP_STEPS;

	public int WJLOCK_STEPS_SHORT;

	public int WJLOCK_STEPS_LONG;

	public float WJ_KICKOFF_SPEED;

	public int WALL_STICKY_STEPS;

	public float DASH_SPEED;

	public float DASH_SPEED_SHARP;

	public float DASH_TIME;

	public int DASH_QUEUE_STEPS;

	public float BACK_DASH_SPEED;

	public float BACK_DASH_TIME;

	public float SHADOW_DASH_SPEED;

	public float SHADOW_DASH_TIME;

	public float SHADOW_DASH_COOLDOWN;

	public float SUPER_DASH_SPEED;

	public float DASH_COOLDOWN;

	public float DASH_COOLDOWN_CH;

	public float BACKDASH_COOLDOWN;

	public float WALLSLIDE_SPEED;

	public float WALLSLIDE_DECEL;

	public float NAIL_CHARGE_TIME_DEFAULT;

	public float NAIL_CHARGE_TIME_CHARM;

	public float CYCLONE_HORIZONTAL_SPEED;

	public float SWIM_ACCEL;

	public float SWIM_MAX_SPEED;

	public float TIME_TO_ENTER_SCENE_BOT;

	public float TIME_TO_ENTER_SCENE_HOR;

	public float SPEED_TO_ENTER_SCENE_HOR;

	public float SPEED_TO_ENTER_SCENE_UP;

	public float SPEED_TO_ENTER_SCENE_DOWN;

	public float DEFAULT_GRAVITY;

	public float UNDERWATER_GRAVITY;

	public float ATTACK_DURATION;

	public float ATTACK_DURATION_CH;

	public float ALT_ATTACK_RESET;

	public float ATTACK_RECOVERY_TIME;

	public float ATTACK_COOLDOWN_TIME;

	public float ATTACK_COOLDOWN_TIME_CH;

	public float BOUNCE_TIME;

	public float BOUNCE_SHROOM_TIME;

	public float BOUNCE_VELOCITY;

	public float SHROOM_BOUNCE_VELOCITY;

	public float RECOIL_HOR_TIME;

	public float RECOIL_HOR_VELOCITY;

	public float RECOIL_HOR_VELOCITY_LONG;

	public float RECOIL_HOR_STEPS;

	public float RECOIL_DOWN_VELOCITY;

	public float RUN_PUFF_TIME;

	public float BIG_FALL_TIME;

	public float HARD_LANDING_TIME;

	public float DOWN_DASH_TIME;

	public float MAX_FALL_VELOCITY;

	public float MAX_FALL_VELOCITY_UNDERWATER;

	public float RECOIL_DURATION;

	public float RECOIL_DURATION_STAL;

	public float RECOIL_VELOCITY;

	public float DAMAGE_FREEZE_DOWN;

	public float DAMAGE_FREEZE_WAIT;

	public float DAMAGE_FREEZE_UP;

	public float INVUL_TIME;

	public float INVUL_TIME_STAL;

	public float INVUL_TIME_PARRY;

	public float INVUL_TIME_QUAKE;

	public float INVUL_TIME_CYCLONE;

	public float CAST_TIME;

	public float CAST_RECOIL_TIME;

	public float CAST_RECOIL_VELOCITY;

	public float WALLSLIDE_CLIP_DELAY;

	public int GRUB_SOUL_MP;

	public int GRUB_SOUL_MP_COMBO;

	private int JUMP_QUEUE_STEPS = 2;

	private int JUMP_RELEASE_QUEUE_STEPS = 2;

	private int DOUBLE_JUMP_QUEUE_STEPS = 10;

	private int ATTACK_QUEUE_STEPS = 5;

	private float DELAY_BEFORE_ENTER = 0.1f;

	private float LOOK_DELAY = 0.85f;

	private float LOOK_ANIM_DELAY = 0.25f;

	private float DEATH_WAIT = 3f;

	private float HAZARD_DEATH_CHECK_TIME = 3f;

	private float FLOATING_CHECK_TIME = 0.18f;

	private float NAIL_TERRAIN_CHECK_TIME = 0.12f;

	private float BUMP_VELOCITY = 4f;

	private float BUMP_VELOCITY_DASH = 5f;

	private int LANDING_BUFFER_STEPS = 5;

	private int LEDGE_BUFFER_STEPS = 2;

	private int HEAD_BUMP_STEPS = 3;

	private float MANTIS_CHARM_SCALE = 1.35f;

	private float FIND_GROUND_POINT_DISTANCE = 10f;

	private float FIND_GROUND_POINT_DISTANCE_EXT = 50f;

	public ActorStates hero_state;

	public ActorStates prev_hero_state;

	public HeroTransitionState transitionState;

	public DamageMode damageMode;

	public float move_input;

	public float vertical_input;

	public float controller_deadzone = 0.2f;

	public Vector2 current_velocity;

	private bool isGameplayScene;

	public Vector2 slashOffset;

	public Vector2 upSlashOffset;

	public Vector2 downwardSlashOffset;

	public Vector2 spell1Offset;

	private int jump_steps;

	private int jumped_steps;

	private int doubleJump_steps;

	private float dash_timer;

	private float back_dash_timer;

	private float shadow_dash_timer;

	private float attack_time;

	private float attack_cooldown;

	private Vector2 transition_vel;

	private float altAttackTime;

	private float lookDelayTimer;

	private float bounceTimer;

	private float recoilHorizontalTimer;

	private float runPuffTimer;

	private float hardLandingTimer;

	private float dashLandingTimer;

	private float recoilTimer;

	private int recoilSteps;

	private int landingBufferSteps;

	private int dashQueueSteps;

	private bool dashQueuing;

	private float shadowDashTimer;

	private float dashCooldownTimer;

	private float nailChargeTimer;

	private int wallLockSteps;

	private float wallslideClipTimer;

	private float hardLandFailSafeTimer;

	private float hazardDeathTimer;

	private float floatingBufferTimer;

	private float attackDuration;

	public float parryInvulnTimer;

	[Space(6f)]
	[Header("Slash Prefabs")]
	public GameObject slashPrefab;

	public GameObject slashAltPrefab;

	public GameObject upSlashPrefab;

	public GameObject downSlashPrefab;

	public GameObject wallSlashPrefab;

	public NailSlash normalSlash;

	public NailSlash alternateSlash;

	public NailSlash upSlash;

	public NailSlash downSlash;

	public NailSlash wallSlash;

	public PlayMakerFSM normalSlashFsm;

	public PlayMakerFSM alternateSlashFsm;

	public PlayMakerFSM upSlashFsm;

	public PlayMakerFSM downSlashFsm;

	public PlayMakerFSM wallSlashFsm;

	[Header("Effect Prefabs")]
	[Space(6f)]
	public GameObject nailTerrainImpactEffectPrefab;

	public GameObject spell1Prefab;

	public GameObject takeHitPrefab;

	public GameObject takeHitDoublePrefab;

	public GameObject softLandingEffectPrefab;

	public GameObject hardLandingEffectPrefab;

	public GameObject runEffectPrefab;

	public GameObject backDashPrefab;

	public GameObject jumpEffectPrefab;

	public GameObject jumpTrailPrefab;

	public GameObject fallEffectPrefab;

	public ParticleSystem wallslideDustPrefab;

	public GameObject artChargeEffect;

	public GameObject artChargedEffect;

	public GameObject artChargedFlash;

	public tk2dSpriteAnimator artChargedEffectAnim;

	public GameObject shadowdashBurstPrefab;

	public GameObject shadowdashDownBurstPrefab;

	public GameObject dashParticlesPrefab;

	public GameObject shadowdashParticlesPrefab;

	public GameObject shadowRingPrefab;

	public GameObject shadowRechargePrefab;

	public GameObject dJumpWingsPrefab;

	public GameObject dJumpBurstPrefab;

	public GameObject dJumpFlashPrefab;

	public ParticleSystem dJumpFeathers;

	public GameObject wallPuffPrefab;

	public GameObject sharpShadowPrefab;

	public GameObject grubberFlyBeamPrefabL;

	public GameObject grubberFlyBeamPrefabR;

	public GameObject grubberFlyBeamPrefabU;

	public GameObject grubberFlyBeamPrefabD;

	public GameObject grubberFlyBeamPrefabL_fury;

	public GameObject grubberFlyBeamPrefabR_fury;

	public GameObject grubberFlyBeamPrefabU_fury;

	public GameObject grubberFlyBeamPrefabD_fury;

	[Space(6f)]
	[Header("Hero Death")]
	public GameObject corpsePrefab;

	public GameObject spikeDeathPrefab;

	public GameObject acidDeathPrefab;

	public GameObject lavaDeathPrefab;

	public GameObject heroDeathPrefab;

	[Space(6f)]
	[Header("Hero Other")]
	public GameObject cutscenePrefab;

	private GameManager gm;

	private Rigidbody2D rb2d;

	private Collider2D col2d;

	private MeshRenderer renderer;

	private new Transform transform;

	private HeroAnimationController animCtrl;

	public HeroControllerStates cState;

	public PlayerData playerData;

	private HeroAudioController audioCtrl;

	private AudioSource audioSource;

	[HideInInspector]
	public UIManager ui;

	private InputHandler inputHandler;

	public PlayMakerFSM damageEffectFSM;

	private ParticleSystem dashParticleSystem;

	private InvulnerablePulse invPulse;

	private SpriteFlash spriteFlash;

	public AudioSource footStepsRunAudioSource;

	public AudioSource footStepsWalkAudioSource;

	private float prevGravityScale;

	private Vector2 recoilVector;

	private Vector2 lastInputState;

	public GatePosition gatePosition;

	private bool runMsgSent;

	private bool hardLanded;

	private bool fallRumble;

	public bool acceptingInput;

	private bool fallTrailGenerated;

	private bool drainMP;

	private float drainMP_timer;

	private float drainMP_time;

	private float MP_drained;

	private float drainMP_seconds;

	private float focusMP_amount;

	private float dashBumpCorrection;

	public bool controlReqlinquished;

	public bool enterWithoutInput;

	public bool lookingUpAnim;

	public bool lookingDownAnim;

	private EndBeta endBeta;

	private int jumpQueueSteps;

	private bool jumpQueuing;

	private int doubleJumpQueueSteps;

	private bool doubleJumpQueuing;

	private int jumpReleaseQueueSteps;

	private bool jumpReleaseQueuing;

	private int attackQueueSteps;

	private bool attackQueuing;

	public bool touchingWallL;

	public bool touchingWallR;

	private bool wallSlidingL;

	private bool wallSlidingR;

	private bool airDashed;

	public bool dashingDown;

	public bool wieldingLantern;

	private bool startWithWallslide;

	private bool startWithJump;

	private bool startWithFullJump;

	private bool startWithDash;

	private bool startWithAttack;

	private bool nailArt_cyclone;

	private bool wallSlashing;

	private bool doubleJumped;

	public bool inAcid;

	private bool wallJumpedR;

	private bool wallJumpedL;

	public bool wallLocked;

	private float currentWalljumpSpeed;

	private float walljumpSpeedDecel;

	private int wallUnstickSteps;

	private bool recoilLarge;

	public float conveyorSpeed;

	public float conveyorSpeedV;

	private bool enteringVertically;

	private bool playingWallslideClip;

	private bool playedMantisClawClip;

	public bool exitedSuperDashing;

	public bool exitedQuake;

	private bool fallCheckFlagged;

	private int ledgeBufferSteps;

	private int headBumpSteps;

	private float nailChargeTime;

	private bool takeNoDamage;

	private bool joniBeam;

	public bool fadedSceneIn;

	private bool stopWalkingOut;

	private bool boundsChecking;

	private bool blockerFix;

	[SerializeField]
	private Vector2[] positionHistory;

	private bool tilemapTestActive;

	private Vector2 groundRayOriginC;

	private Vector2 groundRayOriginL;

	private Vector2 groundRayOriginR;

	private Coroutine takeDamageCoroutine;

	private Coroutine tilemapTestCoroutine;

	public AudioClip footstepsRunDust;

	public AudioClip footstepsRunGrass;

	public AudioClip footstepsRunBone;

	public AudioClip footstepsRunSpa;

	public AudioClip footstepsRunMetal;

	public AudioClip footstepsRunWater;

	public AudioClip footstepsWalkDust;

	public AudioClip footstepsWalkGrass;

	public AudioClip footstepsWalkBone;

	public AudioClip footstepsWalkSpa;

	public AudioClip footstepsWalkMetal;

	public AudioClip nailArtCharge;

	public AudioClip nailArtChargeComplete;

	public AudioClip blockerImpact;

	public AudioClip shadowDashClip;

	public AudioClip sharpShadowClip;

	public AudioClip doubleJumpClip;

	public AudioClip mantisClawClip;

	private GameObject slash;

	private NailSlash slashComponent;

	private PlayMakerFSM slashFsm;

	private GameObject runEffect;

	private GameObject backDash;

	private GameObject jumpEffect;

	private GameObject fallEffect;

	private GameObject dashEffect;

	private GameObject grubberFlyBeam;

	private GameObject hazardCorpe;

	public PlayMakerFSM vignetteFSM;

	public SpriteRenderer heroLight;

	public SpriteRenderer vignette;

	public PlayMakerFSM dashBurst;

	public PlayMakerFSM superDash;

	public PlayMakerFSM fsm_thornCounter;

	public PlayMakerFSM spellControl;

	public PlayMakerFSM fsm_fallTrail;

	private bool jumpReleaseQueueingEnabled;

	private static HeroController _instance;

	public float fallTimer
	{
		get;
		private set;
	}

	public GeoCounter geoCounter
	{
		get;
		private set;
	}

	public PlayMakerFSM proxyFSM
	{
		get;
		private set;
	}

	public TransitionPoint sceneEntryGate
	{
		get;
		private set;
	}

	public static HeroController instance
	{
		get
		{
			if ((UnityEngine.Object)HeroController._instance == (UnityEngine.Object)null)
			{
				HeroController._instance = UnityEngine.Object.FindObjectOfType<HeroController>();
				if ((UnityEngine.Object)HeroController._instance == (UnityEngine.Object)null)
				{
					Debug.LogError("Couldn't find a Hero, make sure one exists in the scene.");
				}
				UnityEngine.Object.DontDestroyOnLoad(HeroController._instance.gameObject);
			}
			return HeroController._instance;
		}
	}

	public event HeroInPosition heroInPosition;

	private void Awake()
	{
		if ((UnityEngine.Object)HeroController._instance == (UnityEngine.Object)null)
		{
			HeroController._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else if ((UnityEngine.Object)this != (UnityEngine.Object)HeroController._instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.SetupGameRefs();
		this.SetupPools();
	}

	private void Start()
	{
		this.playerData = PlayerData.instance;
		this.ui = UIManager.instance;
		this.geoCounter = GameCameras.instance.geoCounter;
		if ((UnityEngine.Object)this.superDash == (UnityEngine.Object)null)
		{
			Debug.Log("SuperDash came up null, locating manually");
			this.superDash = FSMUtility.LocateFSM(base.gameObject, "Superdash");
		}
		if ((UnityEngine.Object)this.fsm_thornCounter == (UnityEngine.Object)null)
		{
			Debug.Log("Thorn Counter came up null, locating manually");
			this.fsm_thornCounter = FSMUtility.LocateFSM(this.transform.FindChild("Charm Effects").gameObject, "Thorn Counter");
		}
		if ((UnityEngine.Object)this.dashBurst == (UnityEngine.Object)null)
		{
			Debug.Log("DashBurst came up null, locating manually");
			this.dashBurst = FSMUtility.GetFSM(this.transform.Find("Effects").Find("Dash Burst").gameObject);
		}
		if ((UnityEngine.Object)this.spellControl == (UnityEngine.Object)null)
		{
			Debug.Log("SpellControl came up null, locating manually");
			this.spellControl = FSMUtility.LocateFSM(base.gameObject, "Spell Control");
		}
		if (this.playerData.equippedCharm_26)
		{
			this.nailChargeTime = this.NAIL_CHARGE_TIME_CHARM;
		}
		else
		{
			this.nailChargeTime = this.NAIL_CHARGE_TIME_DEFAULT;
		}
		if (this.gm.IsGameplayScene())
		{
			this.isGameplayScene = true;
			this.vignette.enabled = true;
			if (this.heroInPosition != null)
			{
				this.heroInPosition(false);
			}
			this.FinishedEnteringScene(true);
		}
		else
		{
			this.isGameplayScene = false;
			this.transform.SetPositionY(-2000f);
			this.vignette.enabled = false;
			this.AffectedByGravity(false);
		}
		this.CharmUpdate();
	}

	public void SceneInit()
	{
		if ((UnityEngine.Object)this == (UnityEngine.Object)HeroController._instance)
		{
			if (!(bool)this.gm)
			{
				this.gm = GameManager.instance;
			}
			if (this.gm.IsGameplayScene())
			{
				this.isGameplayScene = true;
				HeroBox.inactive = false;
			}
			else
			{
				this.isGameplayScene = false;
				this.acceptingInput = false;
				this.SetState(ActorStates.idle);
				this.transform.SetPositionY(-2000f);
				this.vignette.enabled = false;
				this.AffectedByGravity(false);
			}
			this.transform.SetPositionZ(0.004f);
			if (!this.blockerFix)
			{
				if (this.playerData.killedBlocker)
				{
					this.gm.SetPlayerDataInt("killsBlocker", 0);
				}
				this.blockerFix = true;
			}
		}
	}

	private void Update()
	{
		if (Time.frameCount % 10 == 0)
		{
			this.Update10();
		}
		this.current_velocity = this.rb2d.velocity;
		this.FallCheck();
		this.FailSafeChecks();
		if (this.hero_state == ActorStates.running && !this.cState.dashing && !this.cState.backDashing && !this.controlReqlinquished)
		{
			if (this.cState.inWalkZone)
			{
				this.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_RUN);
				this.audioCtrl.PlaySound(HeroSounds.FOOTSTEPS_WALK);
			}
			else
			{
				this.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_WALK);
				this.audioCtrl.PlaySound(HeroSounds.FOOTSTEPS_RUN);
			}
			if (this.runMsgSent)
			{
				Vector2 velocity = this.rb2d.velocity;
				if (velocity.x > -0.1f)
				{
					Vector2 velocity2 = this.rb2d.velocity;
					if (velocity2.x < 0.1f)
					{
						this.runEffect.GetComponent<PlayMakerFSM>().SendEvent("RUN STOP");
						this.runEffect.transform.SetParent(null, true);
						this.runMsgSent = false;
					}
				}
			}
			if (!this.runMsgSent)
			{
				Vector2 velocity3 = this.rb2d.velocity;
				if (!(velocity3.x < -0.1f))
				{
					Vector2 velocity4 = this.rb2d.velocity;
					if (velocity4.x > 0.1f)
					{
						goto IL_0164;
					}
					goto IL_01ee;
				}
				goto IL_0164;
			}
		}
		else
		{
			this.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_RUN);
			this.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_WALK);
			if (this.runMsgSent)
			{
				this.runEffect.GetComponent<PlayMakerFSM>().SendEvent("RUN STOP");
				this.runEffect.transform.SetParent(null, true);
				this.runMsgSent = false;
			}
		}
		goto IL_01ee;
		IL_067a:
		this.LookForQueueInput();
		if (this.drainMP)
		{
			this.drainMP_timer += Time.deltaTime;
			this.drainMP_seconds += Time.deltaTime;
			while (this.drainMP_timer >= this.drainMP_time)
			{
				this.MP_drained += 1f;
				this.drainMP_timer -= this.drainMP_time;
				this.TakeMP(1);
				this.gm.soulOrb_fsm.SendEvent("MP DRAIN");
				if (this.MP_drained == this.focusMP_amount)
				{
					this.MP_drained -= this.drainMP_time;
					this.proxyFSM.SendEvent("HeroCtrl-FocusCompleted");
				}
			}
		}
		if (this.cState.wallSliding)
		{
			if (this.airDashed)
			{
				this.airDashed = false;
			}
			if (this.cState.onGround)
			{
				this.FlipSprite();
				this.CancelWallsliding();
			}
			if (!this.cState.touchingWall)
			{
				this.FlipSprite();
				this.CancelWallsliding();
			}
			if (!this.CanWallSlide())
			{
				this.CancelWallsliding();
			}
			if (!this.playedMantisClawClip)
			{
				this.audioSource.PlayOneShot(this.mantisClawClip, 1f);
				this.playedMantisClawClip = true;
			}
			if (!this.playingWallslideClip)
			{
				if (this.wallslideClipTimer <= this.WALLSLIDE_CLIP_DELAY)
				{
					this.wallslideClipTimer += Time.deltaTime;
				}
				else
				{
					this.wallslideClipTimer = 0f;
					this.audioCtrl.PlaySound(HeroSounds.WALLSLIDE);
					this.playingWallslideClip = true;
				}
			}
		}
		else if (this.playedMantisClawClip)
		{
			this.playedMantisClawClip = false;
		}
		if (!this.cState.wallSliding && this.playingWallslideClip)
		{
			this.audioCtrl.StopSound(HeroSounds.WALLSLIDE);
			this.playingWallslideClip = false;
		}
		if (!this.cState.wallSliding && this.wallslideClipTimer > 0f)
		{
			this.wallslideClipTimer = 0f;
		}
		if (this.wallSlashing && !this.cState.wallSliding)
		{
			this.CancelAttack();
		}
		if (this.attack_cooldown > 0f)
		{
			this.attack_cooldown -= Time.deltaTime;
		}
		if (this.dashCooldownTimer > 0f)
		{
			this.dashCooldownTimer -= Time.deltaTime;
		}
		if (this.shadowDashTimer > 0f)
		{
			this.shadowDashTimer -= Time.deltaTime;
			if (this.shadowDashTimer <= 0f)
			{
				this.spriteFlash.FlashShadowRecharge();
			}
		}
		if (!this.gm.isPaused)
		{
			if (this.inputHandler.inputActions.attack.IsPressed && this.CanNailCharge())
			{
				this.cState.nailCharging = true;
				this.nailChargeTimer += Time.deltaTime;
			}
			else if (this.cState.nailCharging || this.nailChargeTimer != 0f)
			{
				this.artChargeEffect.SetActive(false);
				this.cState.nailCharging = false;
				this.audioCtrl.StopSound(HeroSounds.NAIL_ART_CHARGE);
			}
			if (this.cState.nailCharging && this.nailChargeTimer > 0.5f && !this.artChargeEffect.activeSelf && this.nailChargeTimer < this.nailChargeTime)
			{
				this.artChargeEffect.SetActive(true);
				this.audioCtrl.PlaySound(HeroSounds.NAIL_ART_CHARGE);
			}
			if (this.artChargeEffect.activeSelf && (!this.cState.nailCharging || this.nailChargeTimer > this.nailChargeTime))
			{
				this.artChargeEffect.SetActive(false);
				this.audioCtrl.StopSound(HeroSounds.NAIL_ART_CHARGE);
			}
			if (!this.artChargedEffect.activeSelf && this.nailChargeTimer >= this.nailChargeTime)
			{
				this.artChargedEffect.SetActive(true);
				this.artChargedFlash.SetActive(true);
				this.artChargedEffectAnim.PlayFromFrame(0);
				GameCameras.instance.cameraShakeFSM.SendEvent("EnemyKillShake");
				this.audioSource.PlayOneShot(this.nailArtChargeComplete, 1f);
				this.audioCtrl.PlaySound(HeroSounds.NAIL_ART_READY);
				this.cState.nailCharging = true;
			}
			if (this.artChargedEffect.activeSelf && (this.nailChargeTimer < this.nailChargeTime || !this.cState.nailCharging))
			{
				this.artChargedEffect.SetActive(false);
				this.audioCtrl.StopSound(HeroSounds.NAIL_ART_READY);
			}
		}
		if (this.gm.isPaused && !this.inputHandler.inputActions.attack.IsPressed)
		{
			this.cState.nailCharging = false;
			this.nailChargeTimer = 0f;
		}
		if (this.cState.swimming && !this.CanSwim())
		{
			this.cState.swimming = false;
		}
		if (this.parryInvulnTimer > 0f)
		{
			this.parryInvulnTimer -= Time.deltaTime;
		}
		return;
		IL_01ee:
		if (this.hero_state == ActorStates.dash_landing)
		{
			this.dashLandingTimer += Time.deltaTime;
			if (this.dashLandingTimer > this.DOWN_DASH_TIME)
			{
				this.BackOnGround();
			}
		}
		if (this.hero_state == ActorStates.hard_landing)
		{
			this.hardLandingTimer += Time.deltaTime;
			if (this.hardLandingTimer > this.HARD_LANDING_TIME)
			{
				this.SetState(ActorStates.grounded);
				this.BackOnGround();
			}
		}
		else if (this.hero_state == ActorStates.no_input)
		{
			if (this.cState.recoiling)
			{
				if (!this.playerData.equippedCharm_4 && this.recoilTimer < this.RECOIL_DURATION)
				{
					goto IL_02c2;
				}
				if (this.playerData.equippedCharm_4 && this.recoilTimer < this.RECOIL_DURATION_STAL)
				{
					goto IL_02c2;
				}
				this.CancelDamageRecoil();
				if ((this.prev_hero_state == ActorStates.idle || this.prev_hero_state == ActorStates.running) && !this.CheckTouchingGround())
				{
					this.cState.onGround = false;
					this.SetState(ActorStates.airborne);
					goto IL_0321;
				}
				this.SetState(ActorStates.previous);
				goto IL_0321;
			}
		}
		else if (this.hero_state != ActorStates.no_input)
		{
			this.LookForInput();
			if (this.cState.attacking && !this.cState.dashing)
			{
				this.attack_time += Time.deltaTime;
				if (this.attack_time >= this.attackDuration)
				{
					this.ResetAttacks();
					this.animCtrl.StopAttack();
				}
			}
			if (this.cState.bouncing)
			{
				if (this.bounceTimer < this.BOUNCE_TIME)
				{
					this.bounceTimer += Time.deltaTime;
				}
				else
				{
					this.CancelBounce();
					Rigidbody2D rigidbody2D = this.rb2d;
					Vector2 velocity5 = this.rb2d.velocity;
					rigidbody2D.velocity = new Vector2(velocity5.x, 0f);
				}
			}
			if (this.cState.shroomBouncing && this.current_velocity.y <= 0f)
			{
				this.cState.shroomBouncing = false;
			}
			if (this.hero_state == ActorStates.idle)
			{
				if (!this.controlReqlinquished && !this.gm.isPaused)
				{
					if (this.inputHandler.inputActions.up.IsPressed || this.inputHandler.inputActions.rs_up.IsPressed)
					{
						this.cState.lookingDown = false;
						this.cState.lookingDownAnim = false;
						if (this.lookDelayTimer >= this.LOOK_DELAY || (this.inputHandler.inputActions.rs_up.IsPressed && !this.cState.jumping && !this.cState.dashing))
						{
							this.cState.lookingUp = true;
						}
						else
						{
							this.lookDelayTimer += Time.deltaTime;
						}
						if (this.lookDelayTimer >= this.LOOK_ANIM_DELAY || this.inputHandler.inputActions.rs_up.IsPressed)
						{
							this.cState.lookingUpAnim = true;
						}
						else
						{
							this.cState.lookingUpAnim = false;
						}
					}
					else if (this.inputHandler.inputActions.down.IsPressed || this.inputHandler.inputActions.rs_down.IsPressed)
					{
						this.cState.lookingUp = false;
						this.cState.lookingUpAnim = false;
						if (this.lookDelayTimer >= this.LOOK_DELAY || (this.inputHandler.inputActions.rs_down.IsPressed && !this.cState.jumping && !this.cState.dashing))
						{
							this.cState.lookingDown = true;
						}
						else
						{
							this.lookDelayTimer += Time.deltaTime;
						}
						if (this.lookDelayTimer >= this.LOOK_ANIM_DELAY || this.inputHandler.inputActions.rs_down.IsPressed)
						{
							this.cState.lookingDownAnim = true;
						}
						else
						{
							this.cState.lookingDownAnim = false;
						}
					}
					else
					{
						this.ResetLook();
					}
				}
				this.runPuffTimer = 0f;
			}
		}
		goto IL_067a;
		IL_0164:
		this.runEffect = this.runEffectPrefab.Spawn();
		this.runEffect.transform.SetParent(base.gameObject.transform, false);
		this.runMsgSent = true;
		goto IL_01ee;
		IL_0321:
		this.fsm_thornCounter.SendEvent("THORN COUNTER");
		goto IL_067a;
		IL_02c2:
		this.recoilTimer += Time.deltaTime;
		goto IL_067a;
	}

	private void FixedUpdate()
	{
		if (this.cState.recoilingLeft || this.cState.recoilingRight)
		{
			if ((float)this.recoilSteps <= this.RECOIL_HOR_STEPS)
			{
				this.recoilSteps++;
			}
			else
			{
				this.CancelRecoilHorizontal();
			}
		}
		if (this.hero_state == ActorStates.hard_landing && !this.cState.onConveyor)
		{
			goto IL_0073;
		}
		if (this.hero_state == ActorStates.dash_landing)
		{
			goto IL_0073;
		}
		if (this.hero_state == ActorStates.no_input)
		{
			if (this.cState.transitioning)
			{
				if (this.transitionState == HeroTransitionState.EXITING_SCENE)
				{
					this.AffectedByGravity(false);
					if (!this.stopWalkingOut)
					{
						Rigidbody2D rigidbody2D = this.rb2d;
						float x = this.transition_vel.x;
						float y = this.transition_vel.y;
						Vector2 velocity = this.rb2d.velocity;
						rigidbody2D.velocity = new Vector2(x, y + velocity.y);
					}
				}
				else if (this.transitionState == HeroTransitionState.ENTERING_SCENE)
				{
					this.rb2d.velocity = this.transition_vel;
				}
				else if (this.transitionState == HeroTransitionState.DROPPING_DOWN)
				{
					Rigidbody2D rigidbody2D2 = this.rb2d;
					float x2 = this.transition_vel.x;
					Vector2 velocity2 = this.rb2d.velocity;
					rigidbody2D2.velocity = new Vector2(x2, velocity2.y);
				}
			}
			else if (this.cState.recoiling)
			{
				this.AffectedByGravity(false);
				this.rb2d.velocity = this.recoilVector;
			}
		}
		else if (this.hero_state != ActorStates.no_input)
		{
			if (this.hero_state == ActorStates.running)
			{
				if (this.move_input > 0f)
				{
					if (this.CheckForBump(CollisionSide.right))
					{
						Rigidbody2D rigidbody2D3 = this.rb2d;
						Vector2 velocity3 = this.rb2d.velocity;
						rigidbody2D3.velocity = new Vector2(velocity3.x, this.BUMP_VELOCITY);
					}
				}
				else if (this.move_input < 0f && this.CheckForBump(CollisionSide.left))
				{
					Rigidbody2D rigidbody2D4 = this.rb2d;
					Vector2 velocity4 = this.rb2d.velocity;
					rigidbody2D4.velocity = new Vector2(velocity4.x, this.BUMP_VELOCITY);
				}
			}
			if (!this.cState.backDashing && !this.cState.dashing)
			{
				this.Move(this.move_input);
				if ((!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.wallSliding && !this.wallLocked)
				{
					if (this.move_input > 0f && !this.cState.facingRight)
					{
						this.FlipSprite();
						this.CancelAttack();
					}
					else if (this.move_input < 0f && this.cState.facingRight)
					{
						this.FlipSprite();
						this.CancelAttack();
					}
				}
				if (this.cState.recoilingLeft)
				{
					float num = (!this.recoilLarge) ? this.RECOIL_HOR_VELOCITY : this.RECOIL_HOR_VELOCITY_LONG;
					Vector2 velocity5 = this.rb2d.velocity;
					if (velocity5.x > 0f - num)
					{
						Rigidbody2D rigidbody2D5 = this.rb2d;
						float x3 = 0f - num;
						Vector2 velocity6 = this.rb2d.velocity;
						rigidbody2D5.velocity = new Vector2(x3, velocity6.y);
					}
					else
					{
						Rigidbody2D rigidbody2D6 = this.rb2d;
						Vector2 velocity7 = this.rb2d.velocity;
						float x4 = velocity7.x - num;
						Vector2 velocity8 = this.rb2d.velocity;
						rigidbody2D6.velocity = new Vector2(x4, velocity8.y);
					}
				}
				if (this.cState.recoilingRight)
				{
					float num2 = (!this.recoilLarge) ? this.RECOIL_HOR_VELOCITY : this.RECOIL_HOR_VELOCITY_LONG;
					Vector2 velocity9 = this.rb2d.velocity;
					if (velocity9.x < num2)
					{
						Rigidbody2D rigidbody2D7 = this.rb2d;
						float x5 = num2;
						Vector2 velocity10 = this.rb2d.velocity;
						rigidbody2D7.velocity = new Vector2(x5, velocity10.y);
					}
					else
					{
						Rigidbody2D rigidbody2D8 = this.rb2d;
						Vector2 velocity11 = this.rb2d.velocity;
						float x6 = velocity11.x + num2;
						Vector2 velocity12 = this.rb2d.velocity;
						rigidbody2D8.velocity = new Vector2(x6, velocity12.y);
					}
				}
			}
			if ((this.cState.lookingUp || this.cState.lookingDown) && Mathf.Abs(this.move_input) > 0.6f)
			{
				this.ResetLook();
			}
			if (this.cState.jumping)
			{
				this.Jump();
			}
			if (this.cState.doubleJumping)
			{
				this.DoubleJump();
			}
			if (this.cState.dashing)
			{
				this.Dash();
			}
			if (this.cState.casting)
			{
				if (this.cState.castRecoiling)
				{
					if (this.cState.facingRight)
					{
						this.rb2d.velocity = new Vector2(0f - this.CAST_RECOIL_VELOCITY, 0f);
					}
					else
					{
						this.rb2d.velocity = new Vector2(this.CAST_RECOIL_VELOCITY, 0f);
					}
				}
				else
				{
					this.rb2d.velocity = Vector2.zero;
				}
			}
			if (this.cState.bouncing)
			{
				Rigidbody2D rigidbody2D9 = this.rb2d;
				Vector2 velocity13 = this.rb2d.velocity;
				rigidbody2D9.velocity = new Vector2(velocity13.x, this.BOUNCE_VELOCITY);
			}
			if (!this.cState.shroomBouncing)
			{
				goto IL_0595;
			}
			goto IL_0595;
		}
		goto IL_0734;
		IL_0073:
		this.ResetMotion();
		goto IL_0734;
		IL_0595:
		if (this.wallLocked)
		{
			if (this.wallJumpedR)
			{
				Rigidbody2D rigidbody2D10 = this.rb2d;
				float x7 = this.currentWalljumpSpeed;
				Vector2 velocity14 = this.rb2d.velocity;
				rigidbody2D10.velocity = new Vector2(x7, velocity14.y);
			}
			else if (this.wallJumpedL)
			{
				Rigidbody2D rigidbody2D11 = this.rb2d;
				float x8 = 0f - this.currentWalljumpSpeed;
				Vector2 velocity15 = this.rb2d.velocity;
				rigidbody2D11.velocity = new Vector2(x8, velocity15.y);
			}
			this.wallLockSteps++;
			if (this.wallLockSteps > this.WJLOCK_STEPS_LONG)
			{
				this.wallLocked = false;
			}
			this.currentWalljumpSpeed -= this.walljumpSpeedDecel;
		}
		if (this.cState.wallSliding)
		{
			if (this.wallSlidingL && this.inputHandler.inputActions.right.IsPressed)
			{
				this.wallUnstickSteps++;
			}
			else if (this.wallSlidingR && this.inputHandler.inputActions.left.IsPressed)
			{
				this.wallUnstickSteps++;
			}
			else
			{
				this.wallUnstickSteps = 0;
			}
			if (this.wallUnstickSteps >= this.WALL_STICKY_STEPS)
			{
				this.CancelWallsliding();
			}
			if (this.wallSlidingL)
			{
				if (!this.CheckStillTouchingWall(CollisionSide.left, false))
				{
					this.FlipSprite();
					this.CancelWallsliding();
				}
			}
			else if (this.wallSlidingR && !this.CheckStillTouchingWall(CollisionSide.right, false))
			{
				this.FlipSprite();
				this.CancelWallsliding();
			}
		}
		goto IL_0734;
		IL_0734:
		Vector2 velocity16 = this.rb2d.velocity;
		if (velocity16.y < 0f - this.MAX_FALL_VELOCITY && !this.inAcid && !this.controlReqlinquished && !this.cState.shadowDashing)
		{
			Rigidbody2D rigidbody2D12 = this.rb2d;
			Vector2 velocity17 = this.rb2d.velocity;
			rigidbody2D12.velocity = new Vector2(velocity17.x, 0f - this.MAX_FALL_VELOCITY);
		}
		if (this.jumpQueuing)
		{
			this.jumpQueueSteps++;
		}
		if (this.doubleJumpQueuing)
		{
			this.doubleJumpQueueSteps++;
		}
		if (this.dashQueuing)
		{
			this.dashQueueSteps++;
		}
		if (this.attackQueuing)
		{
			this.attackQueueSteps++;
		}
		if (this.cState.wallSliding && !this.cState.onConveyorV)
		{
			Vector2 velocity18 = this.rb2d.velocity;
			if (velocity18.y > this.WALLSLIDE_SPEED)
			{
				Rigidbody2D rigidbody2D13 = this.rb2d;
				Vector2 velocity19 = this.rb2d.velocity;
				float x9 = velocity19.x;
				Vector2 velocity20 = this.rb2d.velocity;
				rigidbody2D13.velocity = new Vector3(x9, velocity20.y - this.WALLSLIDE_DECEL);
				Vector2 velocity21 = this.rb2d.velocity;
				if (velocity21.y < this.WALLSLIDE_SPEED)
				{
					Rigidbody2D rigidbody2D14 = this.rb2d;
					Vector2 velocity22 = this.rb2d.velocity;
					rigidbody2D14.velocity = new Vector3(velocity22.x, this.WALLSLIDE_SPEED);
				}
			}
			Vector2 velocity23 = this.rb2d.velocity;
			if (velocity23.y < this.WALLSLIDE_SPEED)
			{
				Rigidbody2D rigidbody2D15 = this.rb2d;
				Vector2 velocity24 = this.rb2d.velocity;
				float x10 = velocity24.x;
				Vector2 velocity25 = this.rb2d.velocity;
				rigidbody2D15.velocity = new Vector3(x10, velocity25.y + this.WALLSLIDE_DECEL);
				Vector2 velocity26 = this.rb2d.velocity;
				if (velocity26.y < this.WALLSLIDE_SPEED)
				{
					Rigidbody2D rigidbody2D16 = this.rb2d;
					Vector2 velocity27 = this.rb2d.velocity;
					rigidbody2D16.velocity = new Vector3(velocity27.x, this.WALLSLIDE_SPEED);
				}
			}
		}
		if (this.nailArt_cyclone)
		{
			if (this.inputHandler.inputActions.right.IsPressed && !this.inputHandler.inputActions.left.IsPressed)
			{
				Rigidbody2D rigidbody2D17 = this.rb2d;
				float cYCLONE_HORIZONTAL_SPEED = this.CYCLONE_HORIZONTAL_SPEED;
				Vector2 velocity28 = this.rb2d.velocity;
				rigidbody2D17.velocity = new Vector3(cYCLONE_HORIZONTAL_SPEED, velocity28.y);
			}
			else if (this.inputHandler.inputActions.left.IsPressed && !this.inputHandler.inputActions.right.IsPressed)
			{
				Rigidbody2D rigidbody2D18 = this.rb2d;
				float x11 = 0f - this.CYCLONE_HORIZONTAL_SPEED;
				Vector2 velocity29 = this.rb2d.velocity;
				rigidbody2D18.velocity = new Vector3(x11, velocity29.y);
			}
			else
			{
				Rigidbody2D rigidbody2D19 = this.rb2d;
				Vector2 velocity30 = this.rb2d.velocity;
				rigidbody2D19.velocity = new Vector3(0f, velocity30.y);
			}
		}
		if (this.cState.swimming)
		{
			Rigidbody2D rigidbody2D20 = this.rb2d;
			Vector2 velocity31 = this.rb2d.velocity;
			float x12 = velocity31.x;
			Vector2 velocity32 = this.rb2d.velocity;
			rigidbody2D20.velocity = new Vector3(x12, velocity32.y + this.SWIM_ACCEL);
			Vector2 velocity33 = this.rb2d.velocity;
			if (velocity33.y > this.SWIM_MAX_SPEED)
			{
				Rigidbody2D rigidbody2D21 = this.rb2d;
				Vector2 velocity34 = this.rb2d.velocity;
				rigidbody2D21.velocity = new Vector3(velocity34.x, this.SWIM_MAX_SPEED);
			}
		}
		if (this.cState.superDashOnWall && !this.cState.onConveyorV)
		{
			this.rb2d.velocity = new Vector3(0f, 0f);
		}
		if (this.cState.onConveyor)
		{
			if (this.cState.onGround && !this.cState.superDashing)
			{
				goto IL_0bb2;
			}
			if (this.hero_state == ActorStates.hard_landing)
			{
				goto IL_0bb2;
			}
		}
		goto IL_0c37;
		IL_0bb2:
		if (this.cState.freezeCharge || this.hero_state == ActorStates.hard_landing || this.controlReqlinquished)
		{
			this.rb2d.velocity = new Vector3(0f, 0f);
		}
		Rigidbody2D rigidbody2D22 = this.rb2d;
		Vector2 velocity35 = this.rb2d.velocity;
		float x13 = velocity35.x + this.conveyorSpeed;
		Vector2 velocity36 = this.rb2d.velocity;
		rigidbody2D22.velocity = new Vector2(x13, velocity36.y);
		goto IL_0c37;
		IL_0c37:
		if (this.cState.inConveyorZone)
		{
			if (this.cState.freezeCharge || this.hero_state == ActorStates.hard_landing)
			{
				this.rb2d.velocity = new Vector3(0f, 0f);
			}
			Rigidbody2D rigidbody2D23 = this.rb2d;
			Vector2 velocity37 = this.rb2d.velocity;
			float x14 = velocity37.x + this.conveyorSpeed;
			Vector2 velocity38 = this.rb2d.velocity;
			rigidbody2D23.velocity = new Vector2(x14, velocity38.y);
			this.superDash.SendEvent("SLOPE CANCEL");
		}
		if (this.cState.slidingLeft)
		{
			Vector2 velocity39 = this.rb2d.velocity;
			if (velocity39.x > -5f)
			{
				Rigidbody2D rigidbody2D24 = this.rb2d;
				Vector2 velocity40 = this.rb2d.velocity;
				rigidbody2D24.velocity = new Vector2(-5f, velocity40.y);
			}
		}
		if (this.landingBufferSteps > 0)
		{
			this.landingBufferSteps--;
		}
		if (this.ledgeBufferSteps > 0)
		{
			this.ledgeBufferSteps--;
		}
		if (this.headBumpSteps > 0)
		{
			this.headBumpSteps--;
		}
		if (this.jumpReleaseQueueSteps > 0)
		{
			this.jumpReleaseQueueSteps--;
		}
		this.positionHistory[1] = this.positionHistory[0];
		this.positionHistory[0] = this.transform.position;
		this.cState.wasOnGround = this.cState.onGround;
	}

	private void Update10()
	{
		if (this.isGameplayScene)
		{
			this.OutOfBoundsCheck();
		}
		float scaleX = this.transform.GetScaleX();
		if (scaleX < -1f)
		{
			this.transform.SetScaleX(-1f);
		}
		if (scaleX > 1f)
		{
			this.transform.SetScaleX(1f);
		}
		Vector3 position = this.transform.position;
		if (position.z != 0.004f)
		{
			this.transform.SetPositionZ(0.004f);
		}
	}

	private void OnLevelUnload()
	{
		if ((UnityEngine.Object)this.transform.parent != (UnityEngine.Object)null)
		{
			this.transform.parent = null;
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void OnDisable()
	{
		if ((UnityEngine.Object)this.gm != (UnityEngine.Object)null)
		{
			this.gm.UnloadingLevel -= this.OnLevelUnload;
		}
	}

	private void Move(float move_direction)
	{
		if (this.cState.onGround)
		{
			this.SetState(ActorStates.grounded);
		}
		if (this.acceptingInput && !this.cState.wallSliding)
		{
			if (this.cState.inWalkZone)
			{
				Rigidbody2D rigidbody2D = this.rb2d;
				float x = move_direction * this.WALK_SPEED;
				Vector2 velocity = this.rb2d.velocity;
				rigidbody2D.velocity = new Vector2(x, velocity.y);
			}
			else if (this.inAcid)
			{
				Rigidbody2D rigidbody2D2 = this.rb2d;
				float x2 = move_direction * this.UNDERWATER_SPEED;
				Vector2 velocity2 = this.rb2d.velocity;
				rigidbody2D2.velocity = new Vector2(x2, velocity2.y);
			}
			else
			{
				Rigidbody2D rigidbody2D3 = this.rb2d;
				float x3 = move_direction * this.RUN_SPEED;
				Vector2 velocity3 = this.rb2d.velocity;
				rigidbody2D3.velocity = new Vector2(x3, velocity3.y);
			}
		}
	}

	private void Jump()
	{
		if (this.jump_steps <= this.JUMP_STEPS)
		{
			if (this.inAcid)
			{
				Rigidbody2D rigidbody2D = this.rb2d;
				Vector2 velocity = this.rb2d.velocity;
				rigidbody2D.velocity = new Vector2(velocity.x, this.JUMP_SPEED_UNDERWATER);
			}
			else
			{
				Rigidbody2D rigidbody2D2 = this.rb2d;
				Vector2 velocity2 = this.rb2d.velocity;
				rigidbody2D2.velocity = new Vector2(velocity2.x, this.JUMP_SPEED);
			}
			this.jump_steps++;
			this.jumped_steps++;
			this.ledgeBufferSteps = 0;
		}
		else
		{
			this.CancelJump();
		}
	}

	private void DoubleJump()
	{
		if (this.doubleJump_steps <= this.DOUBLE_JUMP_STEPS)
		{
			if (this.doubleJump_steps > 3)
			{
				Rigidbody2D rigidbody2D = this.rb2d;
				Vector2 velocity = this.rb2d.velocity;
				rigidbody2D.velocity = new Vector2(velocity.x, this.JUMP_SPEED * 1.1f);
			}
			this.doubleJump_steps++;
		}
		else
		{
			this.CancelDoubleJump();
		}
		if (this.cState.onGround)
		{
			this.CancelDoubleJump();
		}
	}

	private void Attack(AttackDirection attackDir)
	{
		if (Time.timeSinceLevelLoad - this.altAttackTime > this.ALT_ATTACK_RESET)
		{
			this.cState.altAttack = false;
		}
		this.cState.attacking = true;
		if (this.playerData.equippedCharm_32)
		{
			this.attackDuration = this.ATTACK_DURATION_CH;
		}
		else
		{
			this.attackDuration = this.ATTACK_DURATION;
		}
		if (this.cState.wallSliding)
		{
			this.wallSlashing = true;
			this.slashComponent = this.wallSlash;
			this.slashFsm = this.wallSlashFsm;
		}
		else
		{
			this.wallSlashing = false;
			Transform t3;
			Vector3 localScale3;
			Transform t5;
			Vector3 localScale5;
			Vector3 localScale7;
			switch (attackDir)
			{
			case AttackDirection.normal:
				if (!this.cState.altAttack)
				{
					this.slashComponent = this.normalSlash;
					this.slashFsm = this.normalSlashFsm;
					this.cState.altAttack = true;
				}
				else
				{
					this.slashComponent = this.alternateSlash;
					this.slashFsm = this.alternateSlashFsm;
					this.cState.altAttack = false;
				}
				if (!this.playerData.equippedCharm_35)
				{
					break;
				}
				if (this.playerData.health == this.playerData.maxHealth && !this.playerData.equippedCharm_27)
				{
					goto IL_0150;
				}
				if (this.joniBeam && this.playerData.equippedCharm_27)
				{
					goto IL_0150;
				}
				goto IL_01ea;
			case AttackDirection.upward:
				this.slashComponent = this.upSlash;
				this.slashFsm = this.upSlashFsm;
				this.cState.upAttacking = true;
				if (!this.playerData.equippedCharm_35)
				{
					break;
				}
				if (this.playerData.health == this.playerData.maxHealth && !this.playerData.equippedCharm_27)
				{
					goto IL_033c;
				}
				if (this.joniBeam && this.playerData.equippedCharm_27)
				{
					goto IL_033c;
				}
				goto IL_03de;
			case AttackDirection.downward:
				{
					this.slashComponent = this.downSlash;
					this.slashFsm = this.downSlashFsm;
					this.cState.downAttacking = true;
					if (!this.playerData.equippedCharm_35)
					{
						break;
					}
					if (this.playerData.health == this.playerData.maxHealth && !this.playerData.equippedCharm_27)
					{
						goto IL_053a;
					}
					if (this.joniBeam && this.playerData.equippedCharm_27)
					{
						goto IL_053a;
					}
					goto IL_05de;
				}
				IL_03de:
				if (this.playerData.health == 1 && this.playerData.equippedCharm_6 && this.playerData.healthBlue < 1)
				{
					this.grubberFlyBeam = this.grubberFlyBeamPrefabU_fury.Spawn(this.transform.position);
					Transform t = this.grubberFlyBeam.transform;
					Vector3 localScale = this.transform.localScale;
					t.SetScaleY(localScale.x);
					this.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 270f);
					if (this.playerData.equippedCharm_13)
					{
						Transform t2 = this.grubberFlyBeam.transform;
						Vector3 localScale2 = this.grubberFlyBeam.transform.localScale;
						t2.SetScaleY(localScale2.y * this.MANTIS_CHARM_SCALE);
					}
				}
				break;
				IL_033c:
				this.grubberFlyBeam = this.grubberFlyBeamPrefabU.Spawn(this.transform.position);
				t3 = this.grubberFlyBeam.transform;
				localScale3 = this.transform.localScale;
				t3.SetScaleY(localScale3.x);
				this.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 270f);
				if (this.playerData.equippedCharm_13)
				{
					Transform t4 = this.grubberFlyBeam.transform;
					Vector3 localScale4 = this.grubberFlyBeam.transform.localScale;
					t4.SetScaleY(localScale4.y * this.MANTIS_CHARM_SCALE);
				}
				goto IL_03de;
				IL_053a:
				this.grubberFlyBeam = this.grubberFlyBeamPrefabD.Spawn(this.transform.position);
				t5 = this.grubberFlyBeam.transform;
				localScale5 = this.transform.localScale;
				t5.SetScaleY(localScale5.x);
				this.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
				if (this.playerData.equippedCharm_13)
				{
					Transform t6 = this.grubberFlyBeam.transform;
					Vector3 localScale6 = this.grubberFlyBeam.transform.localScale;
					t6.SetScaleY(localScale6.y * this.MANTIS_CHARM_SCALE);
				}
				goto IL_05de;
				IL_0150:
				localScale7 = this.transform.localScale;
				if (localScale7.x < 0f)
				{
					this.grubberFlyBeam = this.grubberFlyBeamPrefabR.Spawn(this.transform.position);
				}
				else
				{
					this.grubberFlyBeam = this.grubberFlyBeamPrefabL.Spawn(this.transform.position);
				}
				if (this.playerData.equippedCharm_13)
				{
					this.grubberFlyBeam.transform.SetScaleY(this.MANTIS_CHARM_SCALE);
				}
				else
				{
					this.grubberFlyBeam.transform.SetScaleY(1f);
				}
				goto IL_01ea;
				IL_05de:
				if (this.playerData.health == 1 && this.playerData.equippedCharm_6 && this.playerData.healthBlue < 1)
				{
					this.grubberFlyBeam = this.grubberFlyBeamPrefabD_fury.Spawn(this.transform.position);
					Transform t7 = this.grubberFlyBeam.transform;
					Vector3 localScale8 = this.transform.localScale;
					t7.SetScaleY(localScale8.x);
					this.grubberFlyBeam.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
					if (this.playerData.equippedCharm_13)
					{
						Transform t8 = this.grubberFlyBeam.transform;
						Vector3 localScale9 = this.grubberFlyBeam.transform.localScale;
						t8.SetScaleY(localScale9.y * this.MANTIS_CHARM_SCALE);
					}
				}
				break;
				IL_01ea:
				if (this.playerData.health == 1 && this.playerData.equippedCharm_6 && this.playerData.healthBlue < 1)
				{
					Vector3 localScale10 = this.transform.localScale;
					if (localScale10.x < 0f)
					{
						this.grubberFlyBeam = this.grubberFlyBeamPrefabR_fury.Spawn(this.transform.position);
					}
					else
					{
						this.grubberFlyBeam = this.grubberFlyBeamPrefabL_fury.Spawn(this.transform.position);
					}
					if (this.playerData.equippedCharm_13)
					{
						this.grubberFlyBeam.transform.SetScaleY(this.MANTIS_CHARM_SCALE);
					}
					else
					{
						this.grubberFlyBeam.transform.SetScaleY(1f);
					}
				}
				break;
			}
		}
		if (this.cState.wallSliding)
		{
			if (this.cState.facingRight)
			{
				this.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 180f;
			}
			else
			{
				this.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 0f;
			}
		}
		else if (attackDir == AttackDirection.normal && this.cState.facingRight)
		{
			this.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 0f;
		}
		else if (attackDir == AttackDirection.normal && !this.cState.facingRight)
		{
			this.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 180f;
		}
		else
		{
			switch (attackDir)
			{
			case AttackDirection.upward:
				this.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 90f;
				break;
			case AttackDirection.downward:
				this.slashFsm.FsmVariables.GetFsmFloat("direction").Value = 270f;
				break;
			}
		}
		this.altAttackTime = Time.timeSinceLevelLoad;
		this.slashComponent.StartSlash();
	}

	private void Dash()
	{
		this.AffectedByGravity(false);
		this.ResetHardLandingTimer();
		if (this.dash_timer > this.DASH_TIME)
		{
			this.FinishedDashing();
		}
		else
		{
			float num = (!this.playerData.equippedCharm_16 || !this.cState.shadowDashing) ? this.DASH_SPEED : this.DASH_SPEED_SHARP;
			if (this.dashingDown)
			{
				this.rb2d.velocity = new Vector2(0f, 0f - num);
			}
			else if (this.cState.facingRight)
			{
				if (this.CheckForBump(CollisionSide.right))
				{
					this.rb2d.velocity = new Vector2(num, this.BUMP_VELOCITY_DASH);
				}
				else
				{
					this.rb2d.velocity = new Vector2(num, 0f);
				}
			}
			else if (this.CheckForBump(CollisionSide.left))
			{
				this.rb2d.velocity = new Vector2(0f - num, this.BUMP_VELOCITY_DASH);
			}
			else
			{
				this.rb2d.velocity = new Vector2(0f - num, 0f);
			}
			this.dash_timer += Time.deltaTime;
		}
	}

	private void BackDash()
	{
	}

	private void ShadowDash()
	{
	}

	private void SuperDash()
	{
	}

	public void FaceRight()
	{
		this.cState.facingRight = true;
		Vector3 localScale = this.transform.localScale;
		localScale.x = -1f;
		this.transform.localScale = localScale;
	}

	public void FaceLeft()
	{
		this.cState.facingRight = false;
		Vector3 localScale = this.transform.localScale;
		localScale.x = 1f;
		this.transform.localScale = localScale;
	}

	public void StartMPDrain(float time)
	{
		this.drainMP = true;
		this.drainMP_timer = 0f;
		this.MP_drained = 0f;
		this.drainMP_time = time;
		this.focusMP_amount = (float)this.playerData.GetInt("focusMP_amount");
	}

	public void StopMPDrain()
	{
		this.drainMP = false;
	}

	public void SetBackOnGround()
	{
		this.cState.onGround = true;
	}

	public void SetStartWithWallslide()
	{
		this.startWithWallslide = true;
	}

	public void SetStartWithJump()
	{
		this.startWithJump = true;
	}

	public void SetStartWithFullJump()
	{
		this.startWithFullJump = true;
	}

	public void SetStartWithDash()
	{
		this.startWithDash = true;
	}

	public void SetStartWithAttack()
	{
		this.startWithAttack = true;
	}

	public void SetSuperDashExit()
	{
		this.exitedSuperDashing = true;
	}

	public void SetQuakeExit()
	{
		this.exitedQuake = true;
	}

	public void SetTakeNoDamage()
	{
		this.takeNoDamage = true;
	}

	public void EndTakeNoDamage()
	{
		this.takeNoDamage = false;
	}

	public void SetHeroParent(Transform newParent)
	{
		this.transform.parent = newParent;
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	public void IsSwimming()
	{
		this.cState.swimming = true;
	}

	public void NotSwimming()
	{
		this.cState.swimming = false;
	}

	public void EnableRenderer()
	{
		this.renderer.enabled = true;
	}

	public void ResetAirMoves()
	{
		this.doubleJumped = false;
		this.airDashed = false;
	}

	public void SetConveyorSpeed(float speed)
	{
		this.conveyorSpeed = speed;
	}

	public void SetConveyorSpeedV(float speed)
	{
		this.conveyorSpeedV = speed;
	}

	public void EnterWithoutInput(bool flag)
	{
		this.enterWithoutInput = flag;
	}

	public void SetDarkness(int darkness)
	{
		if (darkness > 0 && this.playerData.hasLantern)
		{
			this.wieldingLantern = true;
		}
		else
		{
			this.wieldingLantern = false;
		}
	}

	public void CancelHeroJump()
	{
		if (this.cState.jumping)
		{
			this.CancelJump();
			this.CancelDoubleJump();
			Vector2 velocity = this.rb2d.velocity;
			if (velocity.y > 0f)
			{
				Rigidbody2D rigidbody2D = this.rb2d;
				Vector2 velocity2 = this.rb2d.velocity;
				rigidbody2D.velocity = new Vector2(velocity2.x, 0f);
			}
		}
	}

	public void CharmUpdate()
	{
		if (this.playerData.equippedCharm_26)
		{
			this.nailChargeTime = this.NAIL_CHARGE_TIME_CHARM;
		}
		else
		{
			this.nailChargeTime = this.NAIL_CHARGE_TIME_DEFAULT;
		}
		if (this.playerData.equippedCharm_23 && !this.playerData.brokenCharm_23)
		{
			this.playerData.maxHealth = this.playerData.maxHealthBase + 2;
			this.playerData.MaxHealth();
		}
		else
		{
			this.playerData.maxHealth = this.playerData.maxHealthBase;
			this.playerData.MaxHealth();
		}
		if (this.playerData.equippedCharm_27)
		{
			this.playerData.joniHealthBlue = (int)((float)this.playerData.maxHealth * 1.5f);
			this.playerData.maxHealth = 1;
			this.playerData.MaxHealth();
			this.joniBeam = true;
		}
		else
		{
			this.playerData.joniHealthBlue = 0;
		}
		this.playerData.UpdateBlueHealth();
	}

	public void checkEnvironment()
	{
		if (this.playerData.environmentType == 0)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunDust;
			this.footStepsWalkAudioSource.clip = this.footstepsWalkDust;
		}
		else if (this.playerData.environmentType == 1)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunGrass;
			this.footStepsWalkAudioSource.clip = this.footstepsWalkGrass;
		}
		else if (this.playerData.environmentType == 2)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunBone;
			this.footStepsWalkAudioSource.clip = this.footstepsWalkBone;
		}
		else if (this.playerData.environmentType == 3)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunSpa;
			this.footStepsWalkAudioSource.clip = this.footstepsWalkSpa;
		}
		else if (this.playerData.environmentType == 4)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunMetal;
			this.footStepsWalkAudioSource.clip = this.footstepsWalkMetal;
		}
		else if (this.playerData.environmentType == 6)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunWater;
			this.footStepsWalkAudioSource.clip = this.footstepsRunWater;
		}
		else if (this.playerData.environmentType == 7)
		{
			this.footStepsRunAudioSource.clip = this.footstepsRunGrass;
			this.footStepsWalkAudioSource.clip = this.footstepsWalkGrass;
		}
	}

	public void SetBenchRespawn(string spawnMarker, string sceneName, int spawnType, bool facingRight)
	{
		this.playerData.SetBenchRespawn(spawnMarker, sceneName, spawnType, facingRight);
	}

	public void SetHazardRespawn(Vector3 position, bool facingRight)
	{
		this.playerData.SetHazardRespawn(position, facingRight);
	}

	public void AddGeo(int amount)
	{
		this.playerData.AddGeo(amount);
		this.geoCounter.AddGeo(amount);
	}

	public void ToZero()
	{
		this.geoCounter.ToZero();
	}

	public void AddGeoQuietly(int amount)
	{
		this.playerData.AddGeo(amount);
	}

	public void AddGeoToCounter(int amount)
	{
		this.geoCounter.AddGeo(amount);
	}

	public void TakeGeo(int amount)
	{
		this.playerData.TakeGeo(amount);
		this.geoCounter.TakeGeo(amount);
	}

	public void UpdateGeo()
	{
		this.geoCounter.UpdateGeo();
	}

	public bool CanInput()
	{
		return this.acceptingInput;
	}

	public void FlipSprite()
	{
		this.cState.facingRight = !this.cState.facingRight;
		Vector3 localScale = this.transform.localScale;
		localScale.x *= -1f;
		this.transform.localScale = localScale;
	}

	public void NailParry()
	{
		this.parryInvulnTimer = this.INVUL_TIME_PARRY;
	}

	public void NailParryRecover()
	{
		this.attackDuration = 0f;
		this.attack_cooldown = 0f;
		this.CancelAttack();
	}

	public void QuakeInvuln()
	{
		this.parryInvulnTimer = this.INVUL_TIME_QUAKE;
	}

	public void CycloneInvuln()
	{
		this.parryInvulnTimer = this.INVUL_TIME_CYCLONE;
	}

	public void TakeDamage(GameObject go, CollisionSide damageSide)
	{
		PlayMakerFSM fsm = FSMUtility.LocateFSM(go, "damages_hero");
		int num = FSMUtility.GetInt(fsm, "damageDealt");
		int @int = FSMUtility.GetInt(fsm, "hazardType");
		bool spawnDamageEffect = true;
		if (num > 0)
		{
			if (this.CanTakeDamage())
			{
				if (this.damageMode == DamageMode.HAZARD_ONLY && @int == 1)
				{
					return;
				}
				if (this.cState.shadowDashing && @int == 1)
				{
					return;
				}
				if (this.parryInvulnTimer > 0f && @int == 1)
				{
					return;
				}
				if (this.playerData.equippedCharm_5 && this.playerData.blockerHits > 0 && @int == 1 && this.cState.focusing)
				{
					this.proxyFSM.SendEvent("HeroCtrl-TookBlockerHit");
					this.audioSource.PlayOneShot(this.blockerImpact, 1f);
					spawnDamageEffect = false;
					num = 0;
				}
				else
				{
					this.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
				}
				this.CancelAttack();
				if (this.cState.wallSliding)
				{
					this.cState.wallSliding = false;
				}
				if (this.cState.touchingWall)
				{
					this.cState.touchingWall = false;
				}
				if (this.cState.recoilingLeft || this.cState.recoilingRight)
				{
					this.CancelRecoilHorizontal();
				}
				if (this.cState.bouncing)
				{
					this.CancelBounce();
					Rigidbody2D rigidbody2D = this.rb2d;
					Vector2 velocity = this.rb2d.velocity;
					rigidbody2D.velocity = new Vector2(velocity.x, 0f);
				}
				if (this.cState.shroomBouncing)
				{
					this.CancelBounce();
					Rigidbody2D rigidbody2D2 = this.rb2d;
					Vector2 velocity2 = this.rb2d.velocity;
					rigidbody2D2.velocity = new Vector2(velocity2.x, 0f);
				}
				this.audioCtrl.PlaySound(HeroSounds.TAKE_HIT);
				if (!this.takeNoDamage && !this.playerData.invinciTest)
				{
					if (this.playerData.overcharmed)
					{
						this.playerData.TakeHealth(num * 2);
					}
					else
					{
						this.playerData.TakeHealth(num);
					}
				}
				if (this.playerData.equippedCharm_3 && num > 0)
				{
					if (this.playerData.equippedCharm_35)
					{
						this.AddMPCharge(this.GRUB_SOUL_MP_COMBO);
					}
					else
					{
						this.AddMPCharge(this.GRUB_SOUL_MP);
					}
				}
				if (this.joniBeam && num > 0)
				{
					this.joniBeam = false;
				}
				if (this.cState.nailCharging || this.nailChargeTimer != 0f)
				{
					this.cState.nailCharging = false;
					this.nailChargeTimer = 0f;
				}
				if (this.playerData.health == 0)
				{
					base.StartCoroutine(this.Die());
				}
				else
				{
					switch (@int)
					{
					case 2:
					{
						Quaternion rotation = go.transform.rotation;
						base.StartCoroutine(this.DieFromHazard(HazardType.SPIKES, rotation.z));
						break;
					}
					case 3:
						base.StartCoroutine(this.DieFromHazard(HazardType.ACID, 0f));
						break;
					case 4:
						Debug.Log("Lava death");
						break;
					case 5:
						base.StartCoroutine(this.DieFromHazard(HazardType.PIT, 0f));
						break;
					default:
						base.StartCoroutine(this.StartRecoil(damageSide, spawnDamageEffect, num));
						break;
					}
				}
			}
			else if (this.cState.invulnerable && !this.cState.hazardDeath && !this.playerData.isInvincible)
			{
				switch (@int)
				{
				case 2:
					this.playerData.TakeHealth(num);
					this.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
					if (this.playerData.health == 0)
					{
						base.StartCoroutine(this.Die());
					}
					else
					{
						Quaternion rotation2 = go.transform.rotation;
						base.StartCoroutine(this.DieFromHazard(HazardType.SPIKES, rotation2.z));
					}
					break;
				case 3:
					this.playerData.TakeHealth(num);
					this.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
					if (this.playerData.health == 0)
					{
						base.StartCoroutine(this.Die());
					}
					else
					{
						base.StartCoroutine(this.DieFromHazard(HazardType.ACID, 0f));
					}
					break;
				case 4:
					Debug.Log("Lava damage");
					break;
				}
			}
		}
	}

	public string GetEntryGateName()
	{
		if ((UnityEngine.Object)this.sceneEntryGate != (UnityEngine.Object)null)
		{
			return this.sceneEntryGate.name;
		}
		return string.Empty;
	}

	public void AddMPCharge(int amount)
	{
		int mPReserve = this.playerData.MPReserve;
		this.playerData.AddMPCharge(amount);
		GameCameras.instance.soulOrbFSM.SendEvent("MP GAIN");
		if (this.playerData.MPReserve != mPReserve)
		{
			this.gm.soulVessel_fsm.SendEvent("MP RESERVE UP");
		}
	}

	public void SoulGain()
	{
		int mPCharge = this.playerData.MPCharge;
		int num;
		if (mPCharge < this.playerData.maxMP)
		{
			num = 11;
			if (this.playerData.equippedCharm_20)
			{
				num += 3;
			}
			if (this.playerData.equippedCharm_21)
			{
				num += 8;
			}
		}
		else
		{
			num = 6;
			if (this.playerData.equippedCharm_20)
			{
				num += 2;
			}
			if (this.playerData.equippedCharm_21)
			{
				num += 6;
			}
		}
		int mPReserve = this.playerData.MPReserve;
		this.playerData.AddMPCharge(num);
		GameCameras.instance.soulOrbFSM.SendEvent("MP GAIN");
		if (this.playerData.MPReserve != mPReserve)
		{
			this.gm.soulVessel_fsm.SendEvent("MP RESERVE UP");
		}
	}

	public void AddMPChargeSpa(int amount)
	{
		int mPReserve = this.playerData.MPReserve;
		this.playerData.AddMPCharge(amount);
		this.gm.soulOrb_fsm.SendEvent("MP GAIN SPA");
		if (this.playerData.MPReserve != mPReserve)
		{
			this.gm.soulVessel_fsm.SendEvent("MP RESERVE UP");
		}
	}

	public void SetMPCharge(int amount)
	{
		this.playerData.MPCharge = amount;
		GameCameras.instance.soulOrbFSM.SendEvent("MP SET");
	}

	public void TakeMP(int amount)
	{
		this.playerData.TakeMP(amount);
		if (amount > 1)
		{
			GameCameras.instance.soulOrbFSM.SendEvent("MP LOSE");
		}
	}

	public void TakeReserveMP(int amount)
	{
		this.playerData.TakeReserveMP(amount);
		this.gm.soulVessel_fsm.SendEvent("MP RESERVE DOWN");
	}

	public void AddHealth(int amount)
	{
		this.playerData.AddHealth(amount);
		this.proxyFSM.SendEvent("HeroCtrl-Healed");
	}

	public void TakeHealth(int amount)
	{
		this.playerData.TakeHealth(amount);
		this.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
	}

	public void MaxHealth()
	{
		this.proxyFSM.SendEvent("HeroCtrl-MaxHealth");
		this.playerData.MaxHealth();
	}

	public void AddToMaxHealth(int amount)
	{
		this.playerData.AddToMaxHealth(amount);
		this.gm.AwardAchievement("PROTECTED");
		if (this.playerData.maxHealthBase == this.playerData.maxHealthCap)
		{
			this.gm.AwardAchievement("MASKED");
		}
	}

	public void ClearMP()
	{
		this.playerData.ClearMP();
	}

	public void AddToMaxMPReserve(int amount)
	{
		this.playerData.AddToMaxMPReserve(amount);
		this.gm.AwardAchievement("SOULFUL");
		if (this.playerData.MPReserveMax == this.playerData.MPReserveCap)
		{
			this.gm.AwardAchievement("WORLDSOUL");
		}
	}

	public void Bounce()
	{
		if (!this.cState.bouncing && !this.cState.shroomBouncing && !this.controlReqlinquished)
		{
			this.doubleJumped = false;
			this.airDashed = false;
			this.cState.bouncing = true;
		}
	}

	public void BounceHigh()
	{
		if (!this.cState.bouncing && !this.controlReqlinquished)
		{
			this.doubleJumped = false;
			this.airDashed = false;
			this.cState.bouncing = true;
			this.bounceTimer = -0.03f;
			Rigidbody2D rigidbody2D = this.rb2d;
			Vector2 velocity = this.rb2d.velocity;
			rigidbody2D.velocity = new Vector2(velocity.x, this.BOUNCE_VELOCITY);
		}
	}

	public void ShroomBounce()
	{
		this.doubleJumped = false;
		this.airDashed = false;
		this.cState.bouncing = false;
		this.cState.shroomBouncing = true;
		Rigidbody2D rigidbody2D = this.rb2d;
		Vector2 velocity = this.rb2d.velocity;
		rigidbody2D.velocity = new Vector2(velocity.x, this.SHROOM_BOUNCE_VELOCITY);
	}

	public void RecoilLeft()
	{
		if (!this.cState.recoilingLeft && !this.cState.recoilingRight && !this.playerData.equippedCharm_14 && !this.controlReqlinquished)
		{
			this.CancelDash();
			this.recoilSteps = 0;
			this.cState.recoilingLeft = true;
			this.cState.recoilingRight = false;
			this.recoilLarge = false;
			Rigidbody2D rigidbody2D = this.rb2d;
			float x = 0f - this.RECOIL_HOR_VELOCITY;
			Vector2 velocity = this.rb2d.velocity;
			rigidbody2D.velocity = new Vector2(x, velocity.y);
		}
	}

	public void RecoilRight()
	{
		if (!this.cState.recoilingLeft && !this.cState.recoilingRight && !this.playerData.equippedCharm_14 && !this.controlReqlinquished)
		{
			this.CancelDash();
			this.recoilSteps = 0;
			this.cState.recoilingRight = true;
			this.cState.recoilingLeft = false;
			this.recoilLarge = false;
			Rigidbody2D rigidbody2D = this.rb2d;
			float rECOIL_HOR_VELOCITY = this.RECOIL_HOR_VELOCITY;
			Vector2 velocity = this.rb2d.velocity;
			rigidbody2D.velocity = new Vector2(rECOIL_HOR_VELOCITY, velocity.y);
		}
	}

	public void RecoilRightLong()
	{
		if (!this.cState.recoilingLeft && !this.cState.recoilingRight && !this.controlReqlinquished)
		{
			this.CancelDash();
			this.ResetAttacks();
			this.recoilSteps = 0;
			this.cState.recoilingRight = true;
			this.cState.recoilingLeft = false;
			this.recoilLarge = true;
			Rigidbody2D rigidbody2D = this.rb2d;
			float rECOIL_HOR_VELOCITY_LONG = this.RECOIL_HOR_VELOCITY_LONG;
			Vector2 velocity = this.rb2d.velocity;
			rigidbody2D.velocity = new Vector2(rECOIL_HOR_VELOCITY_LONG, velocity.y);
		}
	}

	public void RecoilLeftLong()
	{
		if (!this.cState.recoilingLeft && !this.cState.recoilingRight && !this.controlReqlinquished)
		{
			this.CancelDash();
			this.ResetAttacks();
			this.recoilSteps = 0;
			this.cState.recoilingRight = false;
			this.cState.recoilingLeft = true;
			this.recoilLarge = true;
			Rigidbody2D rigidbody2D = this.rb2d;
			float x = 0f - this.RECOIL_HOR_VELOCITY_LONG;
			Vector2 velocity = this.rb2d.velocity;
			rigidbody2D.velocity = new Vector2(x, velocity.y);
		}
	}

	public void RecoilDown()
	{
		this.CancelJump();
		Vector2 velocity = this.rb2d.velocity;
		if (velocity.y > this.RECOIL_DOWN_VELOCITY && !this.controlReqlinquished)
		{
			Rigidbody2D rigidbody2D = this.rb2d;
			Vector2 velocity2 = this.rb2d.velocity;
			rigidbody2D.velocity = new Vector2(velocity2.x, this.RECOIL_DOWN_VELOCITY);
		}
	}

	public void ForceHardLanding()
	{
		if (!this.cState.onGround)
		{
			this.cState.willHardLand = true;
		}
	}

	public void EnterSceneNoGate()
	{
		this.IgnoreInputWithoutReset();
		this.ResetMotion();
		this.airDashed = false;
		this.doubleJumped = false;
		this.ResetHardLandingTimer();
		this.ResetAttacksDash();
		this.AffectedByGravity(false);
		this.sceneEntryGate = null;
		this.SetState(ActorStates.no_input);
		this.transitionState = HeroTransitionState.WAITING_TO_ENTER_LEVEL;
		this.vignetteFSM.SendEvent("RESET");
		if (this.heroInPosition != null)
		{
			this.heroInPosition(false);
		}
		this.gm.FadeSceneIn();
		this.FinishedEnteringScene(true);
	}

	public IEnumerator EnterScene(TransitionPoint enterGate, float delayBeforeEnter)
	{
		this.IgnoreInputWithoutReset();
		this.ResetMotion();
		this.airDashed = false;
		this.doubleJumped = false;
		this.ResetHardLandingTimer();
		this.ResetAttacksDash();
		this.AffectedByGravity(false);
		this.sceneEntryGate = enterGate;
		this.SetState(ActorStates.no_input);
		this.transitionState = HeroTransitionState.WAITING_TO_ENTER_LEVEL;
		this.vignetteFSM.SendEvent("RESET");
		if (!this.cState.transitioning)
		{
			this.cState.transitioning = true;
		}
		this.gatePosition = enterGate.GetGatePosition();
		if (this.gatePosition == GatePosition.top)
		{
			this.cState.onGround = false;
			this.enteringVertically = true;
			this.exitedSuperDashing = false;
			Vector3 position = enterGate.transform.position;
			float x3 = position.x + enterGate.entryOffset.x;
			Vector3 position2 = enterGate.transform.position;
			float y4 = position2.y + enterGate.entryOffset.y;
			this.transform.SetPosition2D(x3, y4);
			if (this.heroInPosition != null)
			{
				this.heroInPosition(false);
			}
			yield return (object)new WaitForSeconds(0.165f);
			this.gm.FadeSceneIn();
			if (delayBeforeEnter > 0f)
			{
				yield return (object)new WaitForSeconds(delayBeforeEnter);
			}
			if (enterGate.entryDelay > 0f)
			{
				yield return (object)new WaitForSeconds(enterGate.entryDelay);
			}
			yield return (object)new WaitForSeconds(0.4f);
			if (this.exitedQuake)
			{
				this.IgnoreInput();
				this.proxyFSM.SendEvent("HeroCtrl-EnterQuake");
				yield return (object)new WaitForSeconds(0.25f);
				this.FinishedEnteringScene(true);
			}
			else
			{
				this.rb2d.velocity = new Vector2(0f, this.SPEED_TO_ENTER_SCENE_DOWN);
				this.transitionState = HeroTransitionState.ENTERING_SCENE;
				this.transitionState = HeroTransitionState.DROPPING_DOWN;
				this.AffectedByGravity(true);
				if (enterGate.hardLandOnExit)
				{
					this.cState.willHardLand = true;
				}
				yield return (object)new WaitForSeconds(0.66f);
				this.transitionState = HeroTransitionState.ENTERING_SCENE;
				if (this.transitionState != 0)
				{
					this.FinishedEnteringScene(true);
				}
			}
		}
		else if (this.gatePosition == GatePosition.bottom)
		{
			this.cState.onGround = false;
			this.enteringVertically = true;
			this.exitedSuperDashing = false;
			if (enterGate.alwaysEnterRight)
			{
				this.FaceRight();
			}
			if (enterGate.alwaysEnterLeft)
			{
				this.FaceLeft();
			}
			Vector3 position3 = enterGate.transform.position;
			float x4 = position3.x + enterGate.entryOffset.x;
			Vector3 position4 = enterGate.transform.position;
			float y3 = position4.y + enterGate.entryOffset.y + 3f;
			this.transform.SetPosition2D(x4, y3);
			if (this.heroInPosition != null)
			{
				this.heroInPosition(false);
			}
			yield return (object)new WaitForSeconds(0.165f);
			this.gm.FadeSceneIn();
			if (delayBeforeEnter > 0f)
			{
				yield return (object)new WaitForSeconds(delayBeforeEnter);
			}
			if (enterGate.entryDelay > 0f)
			{
				yield return (object)new WaitForSeconds(enterGate.entryDelay);
			}
			yield return (object)new WaitForSeconds(0.4f);
			if (this.cState.facingRight)
			{
				this.transition_vel = new Vector2(this.SPEED_TO_ENTER_SCENE_HOR, this.SPEED_TO_ENTER_SCENE_UP);
			}
			else
			{
				this.transition_vel = new Vector2(0f - this.SPEED_TO_ENTER_SCENE_HOR, this.SPEED_TO_ENTER_SCENE_UP);
			}
			this.transitionState = HeroTransitionState.ENTERING_SCENE;
			this.transform.SetPosition2D(x4, y3);
			yield return (object)new WaitForSeconds(this.TIME_TO_ENTER_SCENE_BOT);
			Vector2 velocity = this.rb2d.velocity;
			this.transition_vel = new Vector2(velocity.x, 0f);
			this.AffectedByGravity(true);
			this.transitionState = HeroTransitionState.DROPPING_DOWN;
		}
		else if (this.gatePosition == GatePosition.left)
		{
			this.cState.onGround = true;
			this.enteringVertically = false;
			this.SetState(ActorStates.no_input);
			Vector3 position5 = enterGate.transform.position;
			float x2 = position5.x + enterGate.entryOffset.x;
			float x5 = x2 + 2f;
			Vector3 position6 = enterGate.transform.position;
			float y2 = this.FindGroundPointY(x5, position6.y, false);
			this.transform.SetPosition2D(x2, y2);
			if (this.heroInPosition != null)
			{
				this.heroInPosition(true);
			}
			this.FaceRight();
			yield return (object)new WaitForSeconds(0.165f);
			this.gm.FadeSceneIn();
			if (delayBeforeEnter > 0f)
			{
				yield return (object)new WaitForSeconds(delayBeforeEnter);
			}
			if (enterGate.entryDelay > 0f)
			{
				yield return (object)new WaitForSeconds(enterGate.entryDelay);
			}
			yield return (object)new WaitForSeconds(0.4f);
			if (this.exitedSuperDashing)
			{
				this.IgnoreInput();
				this.proxyFSM.SendEvent("HeroCtrl-EnterSuperDash");
				yield return (object)new WaitForSeconds(0.25f);
				this.FinishedEnteringScene(true);
			}
			else
			{
				this.transition_vel = new Vector2(this.RUN_SPEED, 0f);
				this.transitionState = HeroTransitionState.ENTERING_SCENE;
				yield return (object)new WaitForSeconds(0.33f);
				this.FinishedEnteringScene(true);
			}
		}
		else if (this.gatePosition == GatePosition.right)
		{
			this.cState.onGround = true;
			this.enteringVertically = false;
			this.SetState(ActorStates.no_input);
			Vector3 position7 = enterGate.transform.position;
			float x = position7.x + enterGate.entryOffset.x;
			float x6 = x - 2f;
			Vector3 position8 = enterGate.transform.position;
			float y = this.FindGroundPointY(x6, position8.y, false);
			this.transform.SetPosition2D(x, y);
			if (this.heroInPosition != null)
			{
				this.heroInPosition(true);
			}
			this.FaceLeft();
			yield return (object)new WaitForSeconds(0.165f);
			this.gm.FadeSceneIn();
			if (delayBeforeEnter > 0f)
			{
				yield return (object)new WaitForSeconds(delayBeforeEnter);
			}
			if (enterGate.entryDelay > 0f)
			{
				yield return (object)new WaitForSeconds(enterGate.entryDelay);
			}
			yield return (object)new WaitForSeconds(0.4f);
			if (this.exitedSuperDashing)
			{
				this.IgnoreInput();
				this.proxyFSM.SendEvent("HeroCtrl-EnterSuperDash");
				yield return (object)new WaitForSeconds(0.25f);
				this.FinishedEnteringScene(true);
			}
			else
			{
				this.transition_vel = new Vector2(0f - this.RUN_SPEED, 0f);
				this.transitionState = HeroTransitionState.ENTERING_SCENE;
				yield return (object)new WaitForSeconds(0.33f);
				this.FinishedEnteringScene(true);
			}
		}
		else if (this.gatePosition == GatePosition.door)
		{
			if (enterGate.alwaysEnterRight)
			{
				this.FaceRight();
			}
			if (enterGate.alwaysEnterLeft)
			{
				this.FaceLeft();
			}
			this.cState.onGround = true;
			this.enteringVertically = false;
			this.SetState(ActorStates.idle);
			this.SetState(ActorStates.no_input);
			this.exitedSuperDashing = false;
			this.animCtrl.PlayClip("Idle");
			this.transform.SetPosition2D(this.FindGroundPoint(enterGate.transform.position, false));
			if (this.heroInPosition != null)
			{
				this.heroInPosition(false);
			}
			yield return (object)new WaitForEndOfFrame();
			if (delayBeforeEnter > 0f)
			{
				yield return (object)new WaitForSeconds(delayBeforeEnter);
			}
			if (enterGate.entryDelay > 0f)
			{
				yield return (object)new WaitForSeconds(enterGate.entryDelay);
			}
			yield return (object)new WaitForSeconds(0.4f);
			this.gm.FadeSceneIn();
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			if (enterGate.dontWalkOutOfDoor)
			{
				yield return (object)new WaitForSeconds(0.33f);
			}
			else
			{
				float clipLength = this.animCtrl.GetClipDuration("Exit Door To Idle");
				this.animCtrl.PlayClip("Exit Door To Idle");
				if (clipLength > 0f)
				{
					yield return (object)new WaitForSeconds(clipLength);
				}
				else
				{
					yield return (object)new WaitForSeconds(0.33f);
				}
			}
			this.FinishedEnteringScene(true);
		}
	}

	public void LeaveScene(TransitionPoint gate)
	{
		this.IgnoreInputWithoutReset();
		this.ResetHardLandingTimer();
		this.SetState(ActorStates.no_input);
		this.SetDamageMode(DamageMode.NO_DAMAGE);
		this.transitionState = HeroTransitionState.EXITING_SCENE;
		this.CancelFallEffects();
		this.tilemapTestActive = false;
		this.transform.parent = null;
		this.StopTilemapTest();
		switch (gate.GetGatePosition())
		{
		case GatePosition.top:
			this.transition_vel = new Vector2(0f, this.MIN_JUMP_SPEED);
			this.cState.onGround = false;
			break;
		case GatePosition.right:
			this.transition_vel = new Vector2(this.RUN_SPEED, 0f);
			break;
		case GatePosition.bottom:
			this.transition_vel = Vector2.zero;
			this.cState.onGround = false;
			break;
		case GatePosition.left:
			this.transition_vel = new Vector2(0f - this.RUN_SPEED, 0f);
			break;
		}
		this.cState.transitioning = true;
	}

	public IEnumerator BetaLeave(EndBeta betaEndTrigger)
	{
		if (!this.playerData.betaEnd)
		{
			this.endBeta = betaEndTrigger;
			this.IgnoreInput();
			this.playerData.disablePause = true;
			this.SetState(ActorStates.no_input);
			this.ResetInput();
			this.tilemapTestActive = false;
			yield return (object)new WaitForSeconds(0.66f);
			GameObject.Find("Beta Ender").GetComponent<SimpleSpriteFade>().FadeIn();
			this.ResetMotion();
			yield return (object)new WaitForSeconds(1.25f);
			this.playerData.betaEnd = true;
		}
	}

	public IEnumerator BetaReturn()
	{
		this.rb2d.velocity = new Vector2(this.RUN_SPEED, 0f);
		if (!this.cState.facingRight)
		{
			this.FlipSprite();
		}
		GameObject.Find("Beta Ender").GetComponent<SimpleSpriteFade>().FadeOut();
		this.animCtrl.PlayClip("Run");
		yield return (object)new WaitForSeconds(1.4f);
		this.SetState(ActorStates.grounded);
		this.SetStartingMotionState();
		this.AcceptInput();
		this.playerData.betaEnd = false;
		this.playerData.disablePause = false;
		this.tilemapTestActive = true;
		if ((UnityEngine.Object)this.endBeta != (UnityEngine.Object)null)
		{
			this.endBeta.Reactivate();
		}
	}

	public IEnumerator Respawn()
	{
		this.playerData = PlayerData.instance;
		this.playerData.disablePause = true;
		base.gameObject.layer = 9;
		this.renderer.enabled = true;
		this.rb2d.isKinematic = false;
		this.cState.dead = false;
		this.cState.onGround = true;
		this.cState.hazardDeath = false;
		this.enteringVertically = false;
		this.airDashed = false;
		this.doubleJumped = false;
		this.CharmUpdate();
		this.MaxHealth();
		this.ClearMP();
		this.ResetMotion();
		this.ResetHardLandingTimer();
		this.ResetAttacks();
		this.ResetInput();
		this.CharmUpdate();
		Transform spawnPoint = this.LocateSpawnPoint();
		if ((UnityEngine.Object)spawnPoint != (UnityEngine.Object)null)
		{
			this.transform.SetPosition2D(this.FindGroundPoint(spawnPoint.transform.position, false));
			PlayMakerFSM benchFSM2 = ((Component)spawnPoint).GetComponent<PlayMakerFSM>();
			if ((UnityEngine.Object)benchFSM2 != (UnityEngine.Object)null)
			{
				FSMUtility.GetVector3(benchFSM2, "Adjust Vector");
			}
			else if (this.verboseMode)
			{
				Debug.Log("Could not find Bench Control FSM on respawn point. Ignoring Adjustment offset.");
			}
		}
		else
		{
			Debug.LogError("Couldn't find the respawn point named " + this.playerData.respawnMarkerName + " within objects tagged with RespawnPoint");
		}
		if (this.verboseMode)
		{
			Debug.Log("HC Respawn Type: " + this.playerData.respawnType);
		}
		if (this.playerData.respawnType == 1)
		{
			this.AffectedByGravity(false);
			PlayMakerFSM benchFSM = FSMUtility.LocateFSM(spawnPoint.gameObject, "Bench Control");
			if ((UnityEngine.Object)benchFSM == (UnityEngine.Object)null)
			{
				Debug.LogError("HeroCtrl: Could not find Bench Control FSM on this spawn point, respawn type is set to Bench");
				yield break;
			}
			benchFSM.FsmVariables.GetFsmBool("RespawnResting").Value = true;
			yield return (object)new WaitForEndOfFrame();
			if (this.heroInPosition != null)
			{
				this.heroInPosition(false);
			}
			this.proxyFSM.SendEvent("HeroCtrl-Respawned");
			this.gm.FinishedEnteringScene();
		}
		else
		{
			yield return (object)new WaitForEndOfFrame();
			this.IgnoreInput();
			RespawnMarker respawnMarker = ((Component)spawnPoint).GetComponent<RespawnMarker>();
			if ((bool)respawnMarker)
			{
				if (respawnMarker.respawnFacingRight)
				{
					this.FaceRight();
				}
				else
				{
					this.FaceLeft();
				}
			}
			else
			{
				Debug.LogError("Spawn point does not contain a RespawnMarker");
			}
			if (this.heroInPosition != null)
			{
				this.heroInPosition(false);
			}
			float clipLength = this.animCtrl.GetClipDuration("Wake Up Ground");
			this.animCtrl.PlayClip("Wake Up Ground");
			yield return (object)new WaitForSeconds(clipLength);
			this.proxyFSM.SendEvent("HeroCtrl-Respawned");
			this.FinishedEnteringScene(true);
		}
		this.playerData.disablePause = false;
		this.playerData.isInvincible = false;
	}

	public IEnumerator HazardRespawn()
	{
		this.cState.hazardDeath = false;
		this.cState.onGround = true;
		this.cState.hazardRespawning = true;
		this.ResetMotion();
		this.ResetHardLandingTimer();
		this.ResetAttacks();
		this.ResetInput();
		this.cState.recoiling = false;
		this.enteringVertically = false;
		this.airDashed = false;
		this.doubleJumped = false;
		this.transform.SetPosition2D(this.FindGroundPoint(this.playerData.hazardRespawnLocation, true));
		base.gameObject.layer = 9;
		this.renderer.enabled = true;
		yield return (object)new WaitForEndOfFrame();
		if (this.playerData.hazardRespawnFacingRight)
		{
			this.FaceRight();
		}
		else
		{
			this.FaceLeft();
		}
		if (this.heroInPosition != null)
		{
			this.heroInPosition(false);
		}
		base.StartCoroutine(this.Invulnerable(this.INVUL_TIME * 2f));
		float clipLength = this.animCtrl.GetClipDuration("Hazard Respawn");
		this.animCtrl.PlayClip("Hazard Respawn");
		yield return (object)new WaitForSeconds(clipLength);
		this.cState.hazardRespawning = false;
		this.FinishedEnteringScene(false);
	}

	public void StartCyclone()
	{
		this.nailArt_cyclone = true;
	}

	public void EndCyclone()
	{
		this.nailArt_cyclone = false;
	}

	public bool GetState(string stateName)
	{
		return this.cState.GetState(stateName);
	}

	public bool GetCState(string stateName)
	{
		return this.cState.GetState(stateName);
	}

	public void SetCState(string stateName, bool value)
	{
		this.cState.SetState(stateName, value);
	}

	public void ResetHardLandingTimer()
	{
		this.cState.willHardLand = false;
		this.hardLandingTimer = 0f;
		this.fallTimer = 0f;
		this.hardLanded = false;
	}

	public void CancelSuperDash()
	{
		this.superDash.SendEvent("SLOPE CANCEL");
	}

	public void RelinquishControlNotVelocity()
	{
		if (!this.controlReqlinquished)
		{
			this.prev_hero_state = ActorStates.idle;
			this.ResetInput();
			this.ResetMotionNotVelocity();
			this.SetState(ActorStates.no_input);
			this.IgnoreInput();
			this.controlReqlinquished = true;
			this.ResetLook();
			this.ResetAttacks();
			this.touchingWallL = false;
			this.touchingWallR = false;
		}
	}

	public void RelinquishControl()
	{
		if (!this.controlReqlinquished && !this.cState.dead)
		{
			this.ResetInput();
			this.ResetMotion();
			this.IgnoreInput();
			this.controlReqlinquished = true;
			this.ResetLook();
			this.ResetAttacks();
			this.touchingWallL = false;
			this.touchingWallR = false;
		}
	}

	public void RegainControl()
	{
		this.enteringVertically = false;
		if (this.controlReqlinquished && !this.cState.dead)
		{
			this.AffectedByGravity(true);
			this.SetStartingMotionState();
			this.AcceptInput();
			this.controlReqlinquished = false;
			if (this.startWithWallslide)
			{
				this.cState.wallSliding = true;
				this.cState.willHardLand = false;
				this.airDashed = false;
				this.wallslideDustPrefab.enableEmission = true;
				this.startWithWallslide = false;
			}
			else if (this.startWithJump)
			{
				this.HeroJumpNoEffect();
				this.doubleJumpQueuing = false;
				this.startWithJump = false;
			}
			else if (this.startWithFullJump)
			{
				this.HeroJump();
				this.doubleJumpQueuing = false;
				this.startWithFullJump = false;
			}
			else if (this.startWithDash)
			{
				this.HeroDash();
				this.doubleJumpQueuing = false;
				this.startWithDash = false;
			}
			else if (this.startWithAttack)
			{
				this.DoAttack();
				this.doubleJumpQueuing = false;
				this.startWithAttack = false;
			}
			else
			{
				this.cState.touchingWall = false;
				this.touchingWallL = false;
				this.touchingWallR = false;
			}
		}
	}

	public bool CanCast()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.recoiling && !this.cState.recoilFrozen && !this.cState.transitioning && !this.cState.hazardDeath && !this.cState.hazardRespawning && this.CanInput())
		{
			return true;
		}
		return false;
	}

	public bool CanFocus()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.recoiling && this.cState.onGround && !this.cState.transitioning && !this.cState.recoilFrozen && !this.cState.hazardDeath && !this.cState.hazardRespawning && this.CanInput())
		{
			return true;
		}
		return false;
	}

	public bool CanNailArt()
	{
		if (!this.cState.transitioning && !this.cState.attacking && !this.cState.hazardDeath && !this.cState.hazardRespawning && this.nailChargeTimer >= this.nailChargeTime)
		{
			this.nailChargeTimer = 0f;
			return true;
		}
		this.nailChargeTimer = 0f;
		return false;
	}

	public bool CanQuickMap()
	{
		if (!this.gm.isPaused && !this.cState.onConveyor && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.recoiling && !this.cState.transitioning && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.cState.recoilFrozen && this.cState.onGround && this.CanInput())
		{
			return true;
		}
		return false;
	}

	public bool CanInspect()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.recoiling && !this.cState.transitioning && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.cState.recoilFrozen && this.cState.onGround && this.CanInput())
		{
			return true;
		}
		return false;
	}

	public bool CanBackDash()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.preventBackDash && !this.cState.backDashCooldown && !this.controlReqlinquished && !this.cState.recoilFrozen && !this.cState.recoiling && !this.cState.transitioning && this.cState.onGround && this.playerData.canBackDash)
		{
			return true;
		}
		return false;
	}

	public bool CanSuperDash()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.controlReqlinquished && !this.cState.recoilFrozen && !this.cState.recoiling && !this.cState.transitioning && this.playerData.hasSuperDash && (this.cState.onGround || this.cState.wallSliding))
		{
			return true;
		}
		return false;
	}

	public bool CanDreamNail()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.controlReqlinquished && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.cState.recoilFrozen && !this.cState.recoiling && !this.cState.transitioning && this.playerData.hasDreamNail && this.cState.onGround)
		{
			return true;
		}
		return false;
	}

	public bool CanDreamGate()
	{
		if (!this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.controlReqlinquished && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.cState.recoilFrozen && !this.cState.recoiling && !this.cState.transitioning && this.playerData.hasDreamGate && this.cState.onGround)
		{
			return true;
		}
		return false;
	}

	public bool CanInteract()
	{
		if (this.CanInput() && !this.gm.isPaused && !this.cState.dashing && !this.cState.backDashing && !this.cState.attacking && !this.controlReqlinquished && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.cState.recoilFrozen && !this.cState.recoiling && !this.cState.transitioning && this.cState.onGround)
		{
			return true;
		}
		return false;
	}

	public bool CanOpenInventory()
	{
		if (!this.gm.isPaused && !this.controlReqlinquished && !this.cState.transitioning && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.playerData.disablePause && this.CanInput())
		{
			goto IL_0076;
		}
		if (this.playerData.atBench)
		{
			goto IL_0076;
		}
		return false;
		IL_0076:
		return true;
	}

	public void SetDamageMode(int invincibilityType)
	{
		switch (invincibilityType)
		{
		case 0:
			this.damageMode = DamageMode.FULL_DAMAGE;
			break;
		case 1:
			this.damageMode = DamageMode.HAZARD_ONLY;
			break;
		case 2:
			this.damageMode = DamageMode.NO_DAMAGE;
			break;
		}
	}

	public void SetDamageModeFSM(int invincibilityType)
	{
		switch (invincibilityType)
		{
		case 0:
			this.damageMode = DamageMode.FULL_DAMAGE;
			break;
		case 1:
			this.damageMode = DamageMode.HAZARD_ONLY;
			break;
		case 2:
			this.damageMode = DamageMode.NO_DAMAGE;
			break;
		}
	}

	public void ResetQuakeDamage()
	{
		if (this.damageMode == DamageMode.HAZARD_ONLY)
		{
			this.damageMode = DamageMode.FULL_DAMAGE;
		}
	}

	public void SetDamageMode(DamageMode newDamageMode)
	{
		this.damageMode = newDamageMode;
		if (newDamageMode == DamageMode.NO_DAMAGE)
		{
			this.playerData.isInvincible = true;
		}
		else
		{
			this.playerData.isInvincible = false;
		}
	}

	public void StopAnimationControl()
	{
		this.animCtrl.StopControl();
	}

	public void StartAnimationControl()
	{
		this.animCtrl.StartControl();
	}

	public void IgnoreInput()
	{
		if (this.acceptingInput)
		{
			this.acceptingInput = false;
			this.ResetInput();
		}
	}

	public void IgnoreInputWithoutReset()
	{
		if (this.acceptingInput)
		{
			this.acceptingInput = false;
		}
	}

	public void AcceptInput()
	{
		this.acceptingInput = true;
	}

	public void Pause()
	{
		this.PauseInput();
		this.PauseAudio();
		this.JumpReleased();
		this.cState.isPaused = true;
	}

	public void UnPause()
	{
		this.cState.isPaused = false;
		this.UnPauseAudio();
		this.UnPauseInput();
	}

	public void NearBench(bool isNearBench)
	{
		this.cState.nearBench = isNearBench;
	}

	public void SetWalkZone(bool inWalkZone)
	{
		this.cState.inWalkZone = inWalkZone;
	}

	public void ResetState()
	{
		this.cState.Reset();
	}

	public void StopPlayingAudio()
	{
		this.audioCtrl.StopAllSounds();
	}

	public void PauseAudio()
	{
		this.audioCtrl.PauseAllSounds();
	}

	public void UnPauseAudio()
	{
		this.audioCtrl.UnPauseAllSounds();
	}

	private void PauseInput()
	{
		if (this.acceptingInput)
		{
			this.acceptingInput = false;
		}
		this.lastInputState = new Vector2(this.move_input, this.vertical_input);
	}

	private void UnPauseInput()
	{
		if (!this.controlReqlinquished)
		{
			if (this.inputHandler.inputActions.right.IsPressed)
			{
				this.move_input = this.lastInputState.x;
			}
			else if (this.inputHandler.inputActions.left.IsPressed)
			{
				this.move_input = this.lastInputState.x;
			}
			else
			{
				Rigidbody2D rigidbody2D = this.rb2d;
				Vector2 velocity = this.rb2d.velocity;
				rigidbody2D.velocity = new Vector2(0f, velocity.y);
				this.move_input = 0f;
			}
			this.vertical_input = this.lastInputState.y;
			this.acceptingInput = true;
		}
	}

	public void SpawnSoftLandingPrefab()
	{
		this.softLandingEffectPrefab.Spawn(this.transform.position);
	}

	public void AffectedByGravity(bool gravityApplies)
	{
		float gravityScale = this.rb2d.gravityScale;
		if (this.rb2d.gravityScale > Mathf.Epsilon && !gravityApplies)
		{
			this.prevGravityScale = this.rb2d.gravityScale;
			this.rb2d.gravityScale = 0f;
		}
		else if (this.rb2d.gravityScale <= Mathf.Epsilon && gravityApplies)
		{
			this.rb2d.gravityScale = this.prevGravityScale;
			this.prevGravityScale = 0f;
		}
	}

	private void LookForInput()
	{
		if (this.acceptingInput && !this.gm.isPaused && this.isGameplayScene)
		{
			Vector2 vector = this.inputHandler.inputActions.moveVector.Vector;
			this.move_input = vector.x;
			Vector2 vector2 = this.inputHandler.inputActions.moveVector.Vector;
			this.vertical_input = vector2.y;
			this.FilterInput();
			if (this.playerData.hasWalljump && this.CanWallSlide() && !this.cState.attacking)
			{
				if (this.touchingWallL && this.inputHandler.inputActions.left.IsPressed && !this.cState.wallSliding)
				{
					this.airDashed = false;
					this.doubleJumped = false;
					this.cState.wallSliding = true;
					this.cState.willHardLand = false;
					this.wallslideDustPrefab.enableEmission = true;
					this.wallSlidingL = true;
					this.wallSlidingR = false;
					this.FaceLeft();
				}
				if (this.touchingWallR && this.inputHandler.inputActions.right.IsPressed && !this.cState.wallSliding)
				{
					this.airDashed = false;
					this.doubleJumped = false;
					this.cState.wallSliding = true;
					this.cState.willHardLand = false;
					this.wallslideDustPrefab.enableEmission = true;
					this.wallSlidingL = false;
					this.wallSlidingR = true;
					this.FaceRight();
				}
			}
			if (this.cState.wallSliding && this.inputHandler.inputActions.down.WasPressed)
			{
				this.CancelWallsliding();
				this.FlipSprite();
			}
			if (this.wallLocked && this.wallJumpedL && this.inputHandler.inputActions.right.IsPressed && this.wallLockSteps >= this.WJLOCK_STEPS_SHORT)
			{
				this.wallLocked = false;
			}
			if (this.wallLocked && this.wallJumpedR && this.inputHandler.inputActions.left.IsPressed && this.wallLockSteps >= this.WJLOCK_STEPS_SHORT)
			{
				this.wallLocked = false;
			}
			if (this.inputHandler.inputActions.jump.WasReleased && this.jumpReleaseQueueingEnabled)
			{
				this.jumpReleaseQueueSteps = this.JUMP_RELEASE_QUEUE_STEPS;
				this.jumpReleaseQueuing = true;
			}
			if (!this.inputHandler.inputActions.jump.IsPressed)
			{
				this.JumpReleased();
			}
			if (!this.inputHandler.inputActions.dash.IsPressed)
			{
				if (this.cState.preventDash && !this.cState.dashCooldown)
				{
					this.cState.preventDash = false;
				}
				this.dashQueuing = false;
			}
			if (!this.inputHandler.inputActions.attack.IsPressed)
			{
				this.attackQueuing = false;
			}
		}
	}

	private void LookForQueueInput()
	{
		if (this.acceptingInput && !this.gm.isPaused && this.isGameplayScene)
		{
			if (this.inputHandler.inputActions.jump.WasPressed)
			{
				if (this.CanWallJump())
				{
					this.DoWallJump();
				}
				else if (this.CanJump())
				{
					this.HeroJump();
				}
				else if (this.CanDoubleJump())
				{
					this.DoDoubleJump();
				}
				else if (this.CanInfiniteAirJump())
				{
					this.CancelJump();
					this.audioCtrl.PlaySound(HeroSounds.JUMP);
					this.ResetLook();
					this.cState.jumping = true;
				}
				else
				{
					this.jumpQueueSteps = 0;
					this.jumpQueuing = true;
					this.doubleJumpQueueSteps = 0;
					this.doubleJumpQueuing = true;
				}
			}
			if (this.inputHandler.inputActions.dash.WasPressed)
			{
				if (this.CanDash())
				{
					this.HeroDash();
				}
				else
				{
					this.dashQueueSteps = 0;
					this.dashQueuing = true;
				}
			}
			if (this.inputHandler.inputActions.attack.WasPressed)
			{
				if (this.CanAttack())
				{
					this.DoAttack();
				}
				else
				{
					this.attackQueueSteps = 0;
					this.attackQueuing = true;
				}
			}
			if (this.inputHandler.inputActions.jump.IsPressed)
			{
				if (this.jumpQueueSteps <= this.JUMP_QUEUE_STEPS && this.CanJump() && this.jumpQueuing)
				{
					this.HeroJump();
				}
				else if (this.doubleJumpQueueSteps <= this.DOUBLE_JUMP_QUEUE_STEPS && this.CanDoubleJump() && this.doubleJumpQueuing)
				{
					if (this.cState.onGround)
					{
						this.HeroJump();
					}
					else
					{
						this.DoDoubleJump();
					}
				}
				if (this.CanSwim())
				{
					if (this.hero_state != ActorStates.airborne)
					{
						this.SetState(ActorStates.airborne);
					}
					this.cState.swimming = true;
				}
			}
			if (this.inputHandler.inputActions.dash.IsPressed && this.dashQueueSteps <= this.DASH_QUEUE_STEPS && this.CanDash() && this.dashQueuing)
			{
				this.HeroDash();
			}
			if (this.inputHandler.inputActions.attack.IsPressed && this.attackQueueSteps <= this.ATTACK_QUEUE_STEPS && this.CanAttack() && this.attackQueuing)
			{
				this.DoAttack();
			}
		}
	}

	private void HeroJump()
	{
		this.jumpEffectPrefab.Spawn(this.transform.position);
		this.audioCtrl.PlaySound(HeroSounds.JUMP);
		this.ResetLook();
		this.cState.recoiling = false;
		this.cState.jumping = true;
		this.jumpQueueSteps = 0;
		this.jumped_steps = 0;
		this.doubleJumpQueuing = false;
	}

	private void HeroJumpNoEffect()
	{
		this.ResetLook();
		this.jump_steps = 5;
		this.cState.jumping = true;
		this.jumpQueueSteps = 0;
		this.jumped_steps = 0;
		this.jump_steps = 5;
	}

	private void DoWallJump()
	{
		this.wallPuffPrefab.SetActive(true);
		this.jumpTrailPrefab.Spawn(this.transform.position);
		this.audioCtrl.PlaySound(HeroSounds.WALLJUMP);
		this.CancelWallsliding();
		if (this.touchingWallL)
		{
			this.FaceRight();
			this.wallJumpedR = true;
			this.wallJumpedL = false;
		}
		else if (this.touchingWallR)
		{
			this.FaceLeft();
			this.wallJumpedR = false;
			this.wallJumpedL = true;
		}
		this.cState.touchingWall = false;
		this.touchingWallL = false;
		this.touchingWallR = false;
		this.airDashed = false;
		this.doubleJumped = false;
		this.currentWalljumpSpeed = this.WJ_KICKOFF_SPEED;
		this.walljumpSpeedDecel = (this.WJ_KICKOFF_SPEED - this.RUN_SPEED) / (float)this.WJLOCK_STEPS_LONG;
		this.dashBurst.SendEvent("CANCEL");
		this.cState.jumping = true;
		this.wallLockSteps = 0;
		this.wallLocked = true;
		this.jumpQueueSteps = 0;
		this.jumped_steps = 0;
	}

	private void DoDoubleJump()
	{
		this.dJumpWingsPrefab.SetActive(true);
		this.dJumpFlashPrefab.SetActive(true);
		this.dJumpFeathers.Play();
		this.audioSource.PlayOneShot(this.doubleJumpClip, 1f);
		this.ResetLook();
		this.cState.jumping = false;
		this.cState.doubleJumping = true;
		this.doubleJump_steps = 0;
		this.doubleJumped = true;
	}

	private void DoHardLanding()
	{
		this.AffectedByGravity(true);
		this.ResetInput();
		this.SetState(ActorStates.hard_landing);
		this.CancelAttack();
		this.hardLanded = true;
		this.audioCtrl.PlaySound(HeroSounds.HARD_LANDING);
		this.hardLandingEffectPrefab.Spawn(this.transform.position);
	}

	private void DoAttack()
	{
		this.ResetLook();
		this.cState.recoiling = false;
		if (this.playerData.equippedCharm_32)
		{
			this.attack_cooldown = this.ATTACK_COOLDOWN_TIME_CH;
		}
		else
		{
			this.attack_cooldown = this.ATTACK_COOLDOWN_TIME;
		}
		if (this.vertical_input > Mathf.Epsilon)
		{
			this.Attack(AttackDirection.upward);
			base.StartCoroutine(this.CheckForTerrainThunk(AttackDirection.upward));
		}
		else if (this.vertical_input < 0f - Mathf.Epsilon)
		{
			if (this.hero_state != ActorStates.idle && this.hero_state != ActorStates.running)
			{
				this.Attack(AttackDirection.downward);
				base.StartCoroutine(this.CheckForTerrainThunk(AttackDirection.downward));
			}
			else
			{
				this.Attack(AttackDirection.normal);
				base.StartCoroutine(this.CheckForTerrainThunk(AttackDirection.normal));
			}
		}
		else
		{
			this.Attack(AttackDirection.normal);
			base.StartCoroutine(this.CheckForTerrainThunk(AttackDirection.normal));
		}
	}

	private void HeroDash()
	{
		if (!this.cState.onGround && !this.inAcid)
		{
			this.airDashed = true;
		}
		this.ResetAttacksDash();
		this.CancelBounce();
		this.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_RUN);
		this.audioCtrl.StopSound(HeroSounds.FOOTSTEPS_WALK);
		this.audioCtrl.PlaySound(HeroSounds.DASH);
		this.ResetLook();
		this.cState.recoiling = false;
		if (this.cState.wallSliding)
		{
			this.FlipSprite();
		}
		else if (this.inputHandler.inputActions.right.IsPressed)
		{
			this.FaceRight();
		}
		else if (this.inputHandler.inputActions.left.IsPressed)
		{
			this.FaceLeft();
		}
		this.cState.dashing = true;
		this.dashQueueSteps = 0;
		if (!this.cState.onGround && this.inputHandler.inputActions.down.IsPressed && this.playerData.equippedCharm_31)
		{
			this.dashBurst.transform.localPosition = new Vector3(-0.07f, 3.74f, 0.01f);
			this.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			this.dashingDown = true;
		}
		else
		{
			this.dashBurst.transform.localPosition = new Vector3(4.11f, -0.55f, 0.001f);
			this.dashBurst.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.dashingDown = false;
		}
		if (this.playerData.equippedCharm_31)
		{
			this.dashCooldownTimer = this.DASH_COOLDOWN_CH;
		}
		else
		{
			this.dashCooldownTimer = this.DASH_COOLDOWN;
		}
		if (this.playerData.hasShadowDash && this.shadowDashTimer <= 0f)
		{
			this.shadowDashTimer = this.SHADOW_DASH_COOLDOWN;
			this.proxyFSM.SendEvent("HeroCtrl-ShadowDash");
			this.cState.shadowDashing = true;
			if (this.playerData.equippedCharm_16)
			{
				this.audioSource.PlayOneShot(this.sharpShadowClip, 1f);
				this.sharpShadowPrefab.SetActive(true);
			}
			else
			{
				this.audioSource.PlayOneShot(this.shadowDashClip, 1f);
			}
		}
		if (this.cState.shadowDashing)
		{
			if (this.dashingDown)
			{
				GameObject prefab = this.shadowdashDownBurstPrefab;
				Vector3 position = this.transform.position;
				float x = position.x;
				Vector3 position2 = this.transform.position;
				float y = position2.y + 3.5f;
				Vector3 position3 = this.transform.position;
				this.dashEffect = prefab.Spawn(new Vector3(x, y, position3.z + 0.00101f));
				this.dashEffect.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			}
			else
			{
				Vector3 localScale = this.transform.localScale;
				if (localScale.x > 0f)
				{
					GameObject prefab2 = this.shadowdashBurstPrefab;
					Vector3 position4 = this.transform.position;
					float x2 = position4.x + 5.21f;
					Vector3 position5 = this.transform.position;
					float y2 = position5.y - 0.58f;
					Vector3 position6 = this.transform.position;
					this.dashEffect = prefab2.Spawn(new Vector3(x2, y2, position6.z + 0.00101f));
					Transform obj = this.dashEffect.transform;
					Vector3 localScale2 = this.dashEffect.transform.localScale;
					float y3 = localScale2.y;
					Vector3 localScale3 = this.dashEffect.transform.localScale;
					obj.localScale = new Vector3(1.919591f, y3, localScale3.z);
				}
				else
				{
					GameObject prefab3 = this.shadowdashBurstPrefab;
					Vector3 position7 = this.transform.position;
					float x3 = position7.x - 5.21f;
					Vector3 position8 = this.transform.position;
					float y4 = position8.y - 0.58f;
					Vector3 position9 = this.transform.position;
					this.dashEffect = prefab3.Spawn(new Vector3(x3, y4, position9.z + 0.00101f));
					Transform obj2 = this.dashEffect.transform;
					Vector3 localScale4 = this.dashEffect.transform.localScale;
					float y5 = localScale4.y;
					Vector3 localScale5 = this.dashEffect.transform.localScale;
					obj2.localScale = new Vector3(-1.919591f, y5, localScale5.z);
				}
			}
			this.shadowRechargePrefab.SetActive(true);
			FSMUtility.LocateFSM(this.shadowRechargePrefab, "Recharge Effect").SendEvent("RESET");
			this.shadowdashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission = true;
			this.shadowRingPrefab.Spawn(this.transform.position);
		}
		else
		{
			this.dashBurst.SendEvent("PLAY");
			this.dashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission = true;
		}
		if (this.cState.onGround && !this.cState.shadowDashing)
		{
			this.dashEffect = this.backDashPrefab.Spawn(this.transform.position);
			Transform obj3 = this.dashEffect.transform;
			Vector3 localScale6 = this.transform.localScale;
			float x4 = localScale6.x * -1f;
			Vector3 localScale7 = this.transform.localScale;
			float y6 = localScale7.y;
			Vector3 localScale8 = this.transform.localScale;
			obj3.localScale = new Vector3(x4, y6, localScale8.z);
		}
	}

	private void StartFallRumble()
	{
		this.fallRumble = true;
		this.audioCtrl.PlaySound(HeroSounds.FALLING);
		GameCameras.instance.cameraShakeFSM.Fsm.Variables.FindFsmBool("RumblingFall").Value = true;
	}

	private void SetState(ActorStates newState)
	{
		switch (newState)
		{
		case ActorStates.grounded:
			newState = (ActorStates)((!(Mathf.Abs(this.move_input) > Mathf.Epsilon)) ? 1 : 2);
			break;
		case ActorStates.previous:
			newState = this.prev_hero_state;
			break;
		}
		if (newState != this.hero_state)
		{
			this.prev_hero_state = this.hero_state;
			this.hero_state = newState;
			this.animCtrl.UpdateState(newState);
		}
	}

	private void FinishedEnteringScene(bool setHazardMarker = true)
	{
		this.playerData.disablePause = false;
		this.cState.transitioning = false;
		this.transitionState = HeroTransitionState.WAITING_TO_TRANSITION;
		this.stopWalkingOut = false;
		if (this.exitedSuperDashing || this.exitedQuake)
		{
			this.controlReqlinquished = true;
			this.IgnoreInput();
		}
		else
		{
			this.SetStartingMotionState();
			this.AffectedByGravity(true);
		}
		if (setHazardMarker)
		{
			if (this.gm.startedOnThisScene || (UnityEngine.Object)this.sceneEntryGate == (UnityEngine.Object)null)
			{
				this.playerData.SetHazardRespawn(this.transform.position, this.cState.facingRight);
			}
			else if (!this.sceneEntryGate.nonHazardGate)
			{
				this.playerData.SetHazardRespawn(this.sceneEntryGate.respawnMarker);
			}
		}
		if (this.exitedQuake)
		{
			this.SetDamageMode(DamageMode.HAZARD_ONLY);
		}
		else
		{
			this.SetDamageMode(DamageMode.FULL_DAMAGE);
		}
		if (this.enterWithoutInput || this.exitedSuperDashing || this.exitedQuake)
		{
			this.enterWithoutInput = false;
		}
		else
		{
			this.AcceptInput();
		}
		this.gm.FinishedEnteringScene();
		if (this.exitedSuperDashing)
		{
			this.exitedSuperDashing = false;
		}
		if (this.exitedQuake)
		{
			this.exitedQuake = false;
		}
		this.positionHistory[0] = this.transform.position;
		this.positionHistory[1] = this.transform.position;
		this.tilemapTestActive = true;
	}

	private IEnumerator Die()
	{
		if (!this.cState.dead)
		{
			this.playerData.disablePause = true;
			this.boundsChecking = false;
			this.StopTilemapTest();
			this.gm.StoryRecord_death();
			this.cState.onConveyor = false;
			this.cState.onConveyorV = false;
			if (this.gm.GetCurrentMapZone() == "DREAM_WORLD")
			{
				this.RelinquishControl();
				this.StopAnimationControl();
				this.AffectedByGravity(false);
				this.playerData.isInvincible = true;
				this.ResetHardLandingTimer();
				this.renderer.enabled = false;
				this.heroDeathPrefab.SetActive(true);
			}
			else
			{
				if (this.playerData.permadeathMode == 1)
				{
					this.playerData.permadeathMode = 2;
				}
				this.AffectedByGravity(false);
				HeroBox.inactive = true;
				this.rb2d.isKinematic = true;
				this.SetState(ActorStates.no_input);
				this.cState.dead = true;
				this.ResetMotion();
				this.ResetHardLandingTimer();
				this.renderer.enabled = false;
				base.gameObject.layer = 2;
				this.heroDeathPrefab.SetActive(true);
				yield return (object)null;
				base.StartCoroutine(this.gm.PlayerDead(this.DEATH_WAIT));
			}
		}
	}

	private IEnumerator DieFromHazard(HazardType hazardType, float angle)
	{
		if (!this.cState.hazardDeath)
		{
			this.playerData.disablePause = true;
			this.StopTilemapTest();
			this.SetState(ActorStates.no_input);
			this.cState.hazardDeath = true;
			this.ResetMotion();
			this.ResetHardLandingTimer();
			this.AffectedByGravity(false);
			this.renderer.enabled = false;
			base.gameObject.layer = 2;
			switch (hazardType)
			{
			case HazardType.SPIKES:
			{
				GameObject hazardCorpse2 = UnityEngine.Object.Instantiate(this.spikeDeathPrefab);
				hazardCorpse2.transform.position = this.transform.position;
				FSMUtility.SetFloat(hazardCorpse2.GetComponent<PlayMakerFSM>(), "Spike Direction", angle * 57.29578f);
				break;
			}
			case HazardType.ACID:
			{
				GameObject hazardCorpse = UnityEngine.Object.Instantiate(this.acidDeathPrefab);
				hazardCorpse.transform.position = this.transform.position;
				break;
			}
			}
			yield return (object)null;
			base.StartCoroutine(this.gm.PlayerDeadFromHazard(0f));
		}
	}

	private IEnumerator StartRecoil(CollisionSide impactSide, bool spawnDamageEffect, int damageAmount)
	{
		if (!this.cState.recoiling)
		{
			this.playerData.disablePause = true;
			this.ResetMotion();
			this.AffectedByGravity(false);
			switch (impactSide)
			{
			case CollisionSide.left:
				this.recoilVector = new Vector2(this.RECOIL_VELOCITY, this.RECOIL_VELOCITY * 0.5f);
				if (this.cState.facingRight)
				{
					this.FlipSprite();
				}
				break;
			case CollisionSide.right:
				this.recoilVector = new Vector2(0f - this.RECOIL_VELOCITY, this.RECOIL_VELOCITY * 0.5f);
				if (!this.cState.facingRight)
				{
					this.FlipSprite();
				}
				break;
			default:
				this.recoilVector = Vector2.zero;
				break;
			}
			this.SetState(ActorStates.no_input);
			this.cState.recoilFrozen = true;
			if (spawnDamageEffect)
			{
				this.damageEffectFSM.SendEvent("DAMAGE");
				if (damageAmount > 1)
				{
					UnityEngine.Object.Instantiate(this.takeHitDoublePrefab, this.transform.position, this.transform.rotation);
				}
			}
			if (this.playerData.equippedCharm_4)
			{
				base.StartCoroutine(this.Invulnerable(this.INVUL_TIME_STAL));
			}
			else
			{
				base.StartCoroutine(this.Invulnerable(this.INVUL_TIME));
			}
			yield return (object)(this.takeDamageCoroutine = base.StartCoroutine(this.gm.FreezeMoment(this.DAMAGE_FREEZE_DOWN, this.DAMAGE_FREEZE_WAIT, this.DAMAGE_FREEZE_UP, 0.0001f)));
			this.cState.recoilFrozen = false;
			this.cState.recoiling = true;
			this.playerData.disablePause = false;
		}
	}

	private IEnumerator Invulnerable(float duration)
	{
		this.cState.invulnerable = true;
		yield return (object)new WaitForSeconds(this.DAMAGE_FREEZE_DOWN);
		this.invPulse.startInvulnerablePulse();
		yield return (object)new WaitForSeconds(duration);
		this.invPulse.stopInvulnerablePulse();
		this.cState.invulnerable = false;
	}

	private IEnumerator FirstFadeIn()
	{
		yield return (object)new WaitForSeconds(0.25f);
		this.gm.FadeSceneIn();
		this.fadedSceneIn = true;
	}

	private void FallCheck()
	{
		Vector2 velocity = this.rb2d.velocity;
		if (velocity.y <= -1E-06f)
		{
			if (!this.CheckTouchingGround())
			{
				this.cState.falling = true;
				this.cState.onGround = false;
				this.cState.wallJumping = false;
				this.proxyFSM.SendEvent("HeroCtrl-LeftGround");
				if (this.hero_state != ActorStates.no_input)
				{
					this.SetState(ActorStates.airborne);
				}
				if (this.cState.wallSliding)
				{
					this.fallTimer = 0f;
				}
				else
				{
					this.fallTimer += Time.deltaTime;
				}
				if (this.fallTimer > this.BIG_FALL_TIME)
				{
					if (!this.cState.willHardLand)
					{
						this.cState.willHardLand = true;
					}
					if (!this.fallRumble)
					{
						this.StartFallRumble();
					}
				}
				if (this.fallCheckFlagged)
				{
					this.fallCheckFlagged = false;
				}
			}
		}
		else
		{
			this.cState.falling = false;
			this.fallTimer = 0f;
			if (this.transitionState != HeroTransitionState.ENTERING_SCENE)
			{
				this.cState.willHardLand = false;
			}
			if (this.fallCheckFlagged)
			{
				this.fallCheckFlagged = false;
			}
			if (this.fallRumble)
			{
				this.CancelFallEffects();
			}
		}
	}

	private void OutOfBoundsCheck()
	{
		if (this.isGameplayScene)
		{
			Vector2 vector = this.transform.position;
			if (!(vector.y < -60f) && !(vector.y > this.gm.sceneHeight + 60f) && !(vector.x < -60f) && !(vector.x > this.gm.sceneWidth + 60f))
			{
				return;
			}
			if (!this.cState.dead && this.boundsChecking)
			{
				return;
			}
		}
	}

	private void ConfirmOutOfBounds()
	{
		if (this.boundsChecking)
		{
			Debug.Log("Confirming out of bounds");
			Vector2 vector = this.transform.position;
			if (vector.y < -60f || vector.y > this.gm.sceneHeight + 60f || vector.x < -60f || vector.x > this.gm.sceneWidth + 60f)
			{
				if (!this.cState.dead)
				{
					this.rb2d.velocity = Vector2.zero;
					Debug.LogFormat("Pos: {0} Transition State: {1}", this.transform.position, this.transitionState);
				}
			}
			else
			{
				this.boundsChecking = false;
			}
		}
	}

	private void FailSafeChecks()
	{
		if (this.hero_state == ActorStates.hard_landing)
		{
			this.hardLandFailSafeTimer += Time.deltaTime;
			if (this.hardLandFailSafeTimer > this.HARD_LANDING_TIME + 0.3f)
			{
				this.SetState(ActorStates.grounded);
				this.BackOnGround();
				this.hardLandFailSafeTimer = 0f;
			}
		}
		else
		{
			this.hardLandFailSafeTimer = 0f;
		}
		if (this.cState.hazardDeath)
		{
			this.hazardDeathTimer += Time.deltaTime;
			if (this.hazardDeathTimer > this.HAZARD_DEATH_CHECK_TIME && this.hero_state != ActorStates.no_input)
			{
				this.ResetMotion();
				this.AffectedByGravity(false);
				this.SetState(ActorStates.no_input);
				this.hazardDeathTimer = 0f;
			}
		}
		else
		{
			this.hazardDeathTimer = 0f;
		}
		Vector2 velocity = this.rb2d.velocity;
		if (velocity.y == 0f && !this.cState.onGround && !this.cState.falling && !this.cState.jumping && !this.cState.dashing && this.hero_state != ActorStates.hard_landing)
		{
			if (this.CheckTouchingGround())
			{
				this.floatingBufferTimer += Time.deltaTime;
				if (this.floatingBufferTimer > this.FLOATING_CHECK_TIME)
				{
					if (this.cState.recoiling)
					{
						this.CancelDamageRecoil();
					}
					this.BackOnGround();
					this.floatingBufferTimer = 0f;
				}
			}
			else
			{
				this.floatingBufferTimer = 0f;
			}
		}
	}

	private Transform LocateSpawnPoint()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("RespawnPoint");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name == this.playerData.respawnMarkerName)
			{
				return array[i].transform;
			}
		}
		return null;
	}

	private void CancelJump()
	{
		this.cState.jumping = false;
		this.jumpReleaseQueuing = false;
		this.jump_steps = 0;
	}

	private void CancelDoubleJump()
	{
		if (this.dJumpBurstPrefab.activeSelf && this.doubleJump_steps < 3)
		{
			this.dJumpBurstPrefab.transform.SetParent(this.transform);
			this.dJumpBurstPrefab.SetActive(false);
		}
		this.cState.doubleJumping = false;
		this.doubleJump_steps = 0;
	}

	private void CancelDash()
	{
		if (this.cState.shadowDashing)
		{
			this.cState.shadowDashing = false;
			this.proxyFSM.SendEvent("HeroCtrl-ShadowDashEnd");
		}
		this.cState.dashing = false;
		this.dash_timer = 0f;
		this.AffectedByGravity(true);
		this.sharpShadowPrefab.SetActive(false);
		if (this.dashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission)
		{
			this.dashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission = false;
		}
		if (this.shadowdashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission)
		{
			this.shadowdashParticlesPrefab.GetComponent<ParticleSystem>().enableEmission = false;
		}
	}

	private void CancelWallsliding()
	{
		this.wallslideDustPrefab.enableEmission = false;
		this.cState.wallSliding = false;
		this.wallSlidingL = false;
		this.wallSlidingR = false;
	}

	private void CancelBackDash()
	{
		this.cState.backDashing = false;
		this.back_dash_timer = 0f;
	}

	private void CancelDownAttack()
	{
		if (this.cState.downAttacking)
		{
			this.slashComponent.CancelAttack();
			this.ResetAttacks();
		}
	}

	private void CancelAttack()
	{
		if (this.cState.attacking)
		{
			this.slashComponent.CancelAttack();
			this.ResetAttacks();
		}
	}

	private void CancelBounce()
	{
		this.cState.bouncing = false;
		this.cState.shroomBouncing = false;
		this.bounceTimer = 0f;
	}

	private void CancelRecoilHorizontal()
	{
		this.cState.recoilingLeft = false;
		this.cState.recoilingRight = false;
		this.recoilSteps = 0;
	}

	private void CancelDamageRecoil()
	{
		this.cState.recoiling = false;
		this.recoilTimer = 0f;
		this.ResetMotion();
		this.AffectedByGravity(true);
		this.SetDamageMode(DamageMode.FULL_DAMAGE);
	}

	private void CancelFallEffects()
	{
		this.fallRumble = false;
		this.audioCtrl.StopSound(HeroSounds.FALLING);
		GameCameras.instance.cameraShakeFSM.Fsm.Variables.FindFsmBool("RumblingFall").Value = false;
	}

	private void ResetAttacks()
	{
		this.cState.nailCharging = false;
		this.nailChargeTimer = 0f;
		this.cState.attacking = false;
		this.cState.upAttacking = false;
		this.cState.downAttacking = false;
		this.attack_time = 0f;
	}

	private void ResetAttacksDash()
	{
		this.cState.attacking = false;
		this.cState.upAttacking = false;
		this.cState.downAttacking = false;
		this.attack_time = 0f;
	}

	private void ResetMotion()
	{
		this.CancelJump();
		this.CancelDoubleJump();
		this.CancelDash();
		this.CancelBackDash();
		this.CancelBounce();
		this.CancelRecoilHorizontal();
		this.CancelWallsliding();
		this.rb2d.velocity = Vector2.zero;
		this.transition_vel = Vector2.zero;
		this.wallLocked = false;
		this.nailChargeTimer = 0f;
	}

	private void ResetMotionNotVelocity()
	{
		this.CancelJump();
		this.CancelDoubleJump();
		this.CancelDash();
		this.CancelBackDash();
		this.CancelBounce();
		this.CancelRecoilHorizontal();
		this.CancelWallsliding();
		this.transition_vel = Vector2.zero;
		this.wallLocked = false;
	}

	private void ResetLook()
	{
		this.cState.lookingUp = false;
		this.cState.lookingDown = false;
		this.cState.lookingUpAnim = false;
		this.cState.lookingDownAnim = false;
		this.lookDelayTimer = 0f;
	}

	private void ResetInput()
	{
		this.move_input = 0f;
		this.vertical_input = 0f;
	}

	private void BackOnGround()
	{
		if (this.landingBufferSteps <= 0)
		{
			this.landingBufferSteps = this.LANDING_BUFFER_STEPS;
			if (!this.cState.onGround && !this.hardLanded && !this.cState.superDashing)
			{
				this.softLandingEffectPrefab.Spawn(this.transform.position);
			}
		}
		this.cState.falling = false;
		this.fallTimer = 0f;
		this.dashLandingTimer = 0f;
		this.cState.willHardLand = false;
		this.hardLandingTimer = 0f;
		this.hardLanded = false;
		this.jump_steps = 0;
		if (this.cState.doubleJumping)
		{
			this.HeroJump();
		}
		this.SetState(ActorStates.grounded);
		this.cState.onGround = true;
		this.airDashed = false;
		this.doubleJumped = false;
		if (this.dJumpBurstPrefab.activeSelf && this.doubleJump_steps < 3)
		{
			this.dJumpBurstPrefab.transform.SetParent(this.transform);
			this.dJumpBurstPrefab.SetActive(false);
		}
		if (this.dJumpWingsPrefab.activeSelf)
		{
			this.dJumpWingsPrefab.SetActive(false);
		}
	}

	private void JumpReleased()
	{
		Vector2 velocity = this.rb2d.velocity;
		if (velocity.y > 0f && this.jumped_steps >= this.JUMP_STEPS_MIN && !this.inAcid && !this.cState.shroomBouncing)
		{
			if (this.jumpReleaseQueueingEnabled)
			{
				if (this.jumpReleaseQueuing && this.jumpReleaseQueueSteps <= 0)
				{
					Rigidbody2D rigidbody2D = this.rb2d;
					Vector2 velocity2 = this.rb2d.velocity;
					rigidbody2D.velocity = new Vector2(velocity2.x, 0f);
					this.CancelJump();
				}
			}
			else
			{
				Rigidbody2D rigidbody2D2 = this.rb2d;
				Vector2 velocity3 = this.rb2d.velocity;
				rigidbody2D2.velocity = new Vector2(velocity3.x, 0f);
				this.CancelJump();
			}
		}
		this.jumpQueuing = false;
		this.doubleJumpQueuing = false;
		if (this.cState.swimming)
		{
			this.cState.swimming = false;
		}
	}

	private void FinishedDashing()
	{
		this.CancelDash();
		this.AffectedByGravity(true);
		this.animCtrl.FinishedDash();
		this.proxyFSM.SendEvent("HeroCtrl-DashEnd");
		if (this.cState.touchingWall && !this.cState.onGround && this.playerData.hasWalljump)
		{
			this.wallslideDustPrefab.enableEmission = true;
			this.cState.wallSliding = true;
			this.cState.willHardLand = false;
			if (this.touchingWallL)
			{
				this.wallSlidingL = true;
			}
			if (this.touchingWallR)
			{
				this.wallSlidingR = true;
			}
		}
	}

	private void SetStartingMotionState()
	{
		this.move_input = this.inputHandler.inputActions.moveVector.X;
		if (this.CheckTouchingGround())
		{
			this.cState.onGround = true;
			this.SetState(ActorStates.grounded);
			if (this.enteringVertically)
			{
				this.SpawnSoftLandingPrefab();
				this.animCtrl.playLanding = true;
				this.enteringVertically = false;
			}
		}
		else
		{
			this.cState.onGround = false;
			this.SetState(ActorStates.airborne);
		}
		this.animCtrl.UpdateState(this.hero_state);
	}

	[Obsolete("This was used specifically for underwater swimming in acid but is no longer in use.")]
	private void EnterAcid()
	{
		this.rb2d.gravityScale = this.UNDERWATER_GRAVITY;
		this.inAcid = true;
		this.cState.inAcid = true;
	}

	[Obsolete("This was used specifically for underwater swimming in acid but is no longer in use.")]
	private void ExitAcid()
	{
		this.rb2d.gravityScale = this.DEFAULT_GRAVITY;
		this.inAcid = false;
		this.cState.inAcid = false;
		this.airDashed = false;
		this.doubleJumped = false;
		if (this.inputHandler.inputActions.jump.IsPressed)
		{
			this.HeroJump();
		}
	}

	private void TileMapTest()
	{
		if (this.tilemapTestActive && !this.cState.jumping)
		{
			Vector2 vector = this.transform.position;
			Vector2 direction = new Vector2(this.positionHistory[0].x - vector.x, this.positionHistory[0].y - vector.y);
			float magnitude = direction.magnitude;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, direction, magnitude, 256);
			if ((UnityEngine.Object)raycastHit2D.collider != (UnityEngine.Object)null)
			{
				Debug.LogFormat("TERRAIN INGRESS {0} at {1} Jumping: {2}", this.gm.GetSceneNameString(), vector, this.cState.jumping);
				this.ResetMotion();
				this.rb2d.velocity = Vector2.zero;
				if (this.cState.dashing)
				{
					this.FinishedDashing();
					this.transform.SetPosition2D(this.positionHistory[1]);
				}
				if (this.cState.superDashing)
				{
					this.transform.SetPosition2D(raycastHit2D.point);
					this.superDash.SendEvent("HIT WALL");
				}
				if (this.cState.spellQuake)
				{
					this.spellControl.SendEvent("Hero Landed");
					this.transform.SetPosition2D(this.positionHistory[1]);
				}
				this.tilemapTestActive = false;
				this.tilemapTestCoroutine = base.StartCoroutine(this.TilemapTestPause());
			}
		}
	}

	private IEnumerator TilemapTestPause()
	{
		yield return (object)new WaitForSeconds(0.1f);
		this.tilemapTestActive = true;
	}

	private void StopTilemapTest()
	{
		if (this.tilemapTestCoroutine != null)
		{
			base.StopCoroutine(this.tilemapTestCoroutine);
			this.tilemapTestActive = false;
		}
	}

	public IEnumerator CheckForTerrainThunk(AttackDirection attackDir)
	{
		bool terrainHit = false;
		float thunkTimer = this.NAIL_TERRAIN_CHECK_TIME;
		while (thunkTimer > 0f)
		{
			if (!terrainHit)
			{
				float offsetH = 0.25f;
				float castlength = 2.6f;
				float castlengthMultiplier = 1f;
				if (this.playerData.equippedCharm_18)
				{
					castlengthMultiplier += 0.2f;
				}
				if (this.playerData.equippedCharm_13)
				{
					castlengthMultiplier += 0.3f;
				}
				castlength *= castlengthMultiplier;
				Vector2 boxSize = new Vector2(0.45f, 0.45f);
				Vector3 center = this.col2d.bounds.center;
				float x = center.x;
				Vector3 center2 = this.col2d.bounds.center;
				Vector2 rayOriginN = new Vector2(x, center2.y + offsetH);
				Vector3 center3 = this.col2d.bounds.center;
				float x2 = center3.x;
				Vector3 max = this.col2d.bounds.max;
				Vector2 rayOriginT = new Vector2(x2, max.y);
				Vector3 center4 = this.col2d.bounds.center;
				float x3 = center4.x;
				Vector3 min = this.col2d.bounds.min;
				Vector2 rayOriginB = new Vector2(x3, min.y);
				int layerMask = 33554688;
				RaycastHit2D boxCheck = default(RaycastHit2D);
				switch (attackDir)
				{
				case AttackDirection.normal:
					if (this.cState.facingRight && !this.cState.wallSliding)
					{
						goto IL_024b;
					}
					if (!this.cState.facingRight && this.cState.wallSliding)
					{
						goto IL_024b;
					}
					boxCheck = Physics2D.BoxCast(rayOriginN, boxSize, 0f, Vector2.left, castlength, layerMask);
					break;
				case AttackDirection.upward:
					boxCheck = Physics2D.BoxCast(rayOriginT, boxSize, 0f, Vector2.up, castlength, layerMask);
					break;
				case AttackDirection.downward:
					{
						boxCheck = Physics2D.BoxCast(rayOriginB, boxSize, 0f, Vector2.down, castlength, layerMask);
						break;
					}
					IL_024b:
					boxCheck = Physics2D.BoxCast(rayOriginN, boxSize, 0f, Vector2.right, castlength, layerMask);
					break;
				}
				if ((UnityEngine.Object)boxCheck.collider != (UnityEngine.Object)null && !boxCheck.collider.isTrigger)
				{
					NonThunker noThunk = boxCheck.collider.gameObject.GetComponent<NonThunker>();
					if (!((UnityEngine.Object)noThunk != (UnityEngine.Object)null) || (!noThunk.active && true))
					{
						terrainHit = true;
						this.nailTerrainImpactEffectPrefab.Spawn(boxCheck.point, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f)));
						switch (attackDir)
						{
						case AttackDirection.normal:
							if (this.cState.facingRight)
							{
								this.RecoilLeft();
							}
							else
							{
								this.RecoilRight();
							}
							break;
						case AttackDirection.upward:
							this.RecoilDown();
							break;
						}
					}
				}
				thunkTimer -= Time.deltaTime;
			}
			yield return (object)null;
		}
	}

	private bool CheckStillTouchingWall(CollisionSide side, bool checkTop = false)
	{
		Vector3 min = this.col2d.bounds.min;
		float x = min.x;
		Vector3 max = this.col2d.bounds.max;
		Vector2 origin = new Vector2(x, max.y);
		Vector3 min2 = this.col2d.bounds.min;
		float x2 = min2.x;
		Vector3 center = this.col2d.bounds.center;
		Vector2 origin2 = new Vector2(x2, center.y);
		Vector3 min3 = this.col2d.bounds.min;
		float x3 = min3.x;
		Vector3 min4 = this.col2d.bounds.min;
		Vector2 origin3 = new Vector2(x3, min4.y);
		Vector3 max2 = this.col2d.bounds.max;
		float x4 = max2.x;
		Vector3 max3 = this.col2d.bounds.max;
		Vector2 origin4 = new Vector2(x4, max3.y);
		Vector3 max4 = this.col2d.bounds.max;
		float x5 = max4.x;
		Vector3 center2 = this.col2d.bounds.center;
		Vector2 origin5 = new Vector2(x5, center2.y);
		Vector3 max5 = this.col2d.bounds.max;
		float x6 = max5.x;
		Vector3 min5 = this.col2d.bounds.min;
		Vector2 origin6 = new Vector2(x6, min5.y);
		float distance = 0.1f;
		RaycastHit2D raycastHit2D = default(RaycastHit2D);
		RaycastHit2D raycastHit2D2 = default(RaycastHit2D);
		RaycastHit2D raycastHit2D3 = default(RaycastHit2D);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		switch (side)
		{
		case CollisionSide.left:
			if (checkTop)
			{
				raycastHit2D = Physics2D.Raycast(origin, Vector2.left, distance, 256);
			}
			raycastHit2D2 = Physics2D.Raycast(origin2, Vector2.left, distance, 256);
			raycastHit2D3 = Physics2D.Raycast(origin3, Vector2.left, distance, 256);
			break;
		case CollisionSide.right:
			if (checkTop)
			{
				raycastHit2D = Physics2D.Raycast(origin4, Vector2.right, distance, 256);
			}
			raycastHit2D2 = Physics2D.Raycast(origin5, Vector2.right, distance, 256);
			raycastHit2D3 = Physics2D.Raycast(origin6, Vector2.right, distance, 256);
			break;
		default:
			Debug.LogError("Invalid CollisionSide specified.");
			return false;
		}
		if ((UnityEngine.Object)raycastHit2D2.collider != (UnityEngine.Object)null)
		{
			flag2 = true;
			if (raycastHit2D2.collider.isTrigger)
			{
				flag2 = false;
			}
			SteepSlope component = ((Component)raycastHit2D2.collider).GetComponent<SteepSlope>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				flag2 = false;
			}
			NonSlider component2 = ((Component)raycastHit2D2.collider).GetComponent<NonSlider>();
			if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
			{
				flag2 = false;
			}
			if (flag2)
			{
				return true;
			}
		}
		if ((UnityEngine.Object)raycastHit2D3.collider != (UnityEngine.Object)null)
		{
			flag3 = true;
			if (raycastHit2D3.collider.isTrigger)
			{
				flag3 = false;
			}
			SteepSlope component3 = ((Component)raycastHit2D3.collider).GetComponent<SteepSlope>();
			if ((UnityEngine.Object)component3 != (UnityEngine.Object)null)
			{
				flag3 = false;
			}
			NonSlider component4 = ((Component)raycastHit2D3.collider).GetComponent<NonSlider>();
			if ((UnityEngine.Object)component4 != (UnityEngine.Object)null)
			{
				flag3 = false;
			}
			if (flag3)
			{
				return true;
			}
		}
		if (checkTop && (UnityEngine.Object)raycastHit2D.collider != (UnityEngine.Object)null)
		{
			flag = true;
			if (raycastHit2D.collider.isTrigger)
			{
				flag = false;
			}
			SteepSlope component5 = ((Component)raycastHit2D.collider).GetComponent<SteepSlope>();
			if ((UnityEngine.Object)component5 != (UnityEngine.Object)null)
			{
				flag = false;
			}
			NonSlider component6 = ((Component)raycastHit2D.collider).GetComponent<NonSlider>();
			if ((UnityEngine.Object)component6 != (UnityEngine.Object)null)
			{
				flag = false;
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckForBump(CollisionSide side)
	{
		float num = 0.01f;
		Vector3 min = this.col2d.bounds.min;
		float x = min.x;
		Vector3 min2 = this.col2d.bounds.min;
		Vector2 origin = new Vector2(x, min2.y + 0.2f);
		Vector3 min3 = this.col2d.bounds.min;
		float x2 = min3.x;
		Vector3 min4 = this.col2d.bounds.min;
		Vector2 origin2 = new Vector2(x2, min4.y - num);
		Vector3 max = this.col2d.bounds.max;
		float x3 = max.x;
		Vector3 min5 = this.col2d.bounds.min;
		Vector2 origin3 = new Vector2(x3, min5.y + 0.2f);
		Vector3 max2 = this.col2d.bounds.max;
		float x4 = max2.x;
		Vector3 min6 = this.col2d.bounds.min;
		Vector2 origin4 = new Vector2(x4, min6.y - num);
		float distance = 0.32f;
		RaycastHit2D raycastHit2D = default(RaycastHit2D);
		RaycastHit2D raycastHit2D2 = default(RaycastHit2D);
		switch (side)
		{
		case CollisionSide.left:
			raycastHit2D2 = Physics2D.Raycast(origin2, Vector2.left, distance, 256);
			raycastHit2D = Physics2D.Raycast(origin, Vector2.left, distance, 256);
			break;
		case CollisionSide.right:
			raycastHit2D2 = Physics2D.Raycast(origin4, Vector2.right, distance, 256);
			raycastHit2D = Physics2D.Raycast(origin3, Vector2.right, distance, 256);
			break;
		default:
			Debug.LogError("Invalid CollisionSide specified.");
			break;
		}
		if ((UnityEngine.Object)raycastHit2D2.collider != (UnityEngine.Object)null && (UnityEngine.Object)raycastHit2D.collider == (UnityEngine.Object)null)
		{
			return true;
		}
		return false;
	}

	public bool CheckNearRoof()
	{
		Vector2 origin = this.col2d.bounds.max;
		Vector3 min = this.col2d.bounds.min;
		float x = min.x;
		Vector3 max = this.col2d.bounds.max;
		Vector2 origin2 = new Vector2(x, max.y);
		Vector3 center = this.col2d.bounds.center;
		float x2 = center.x;
		Vector3 max2 = this.col2d.bounds.max;
		Vector2 vector = new Vector2(x2, max2.y);
		Vector3 center2 = this.col2d.bounds.center;
		float x3 = center2.x;
		Vector3 size = this.col2d.bounds.size;
		float x4 = x3 + size.x / 4f;
		Vector3 max3 = this.col2d.bounds.max;
		Vector2 origin3 = new Vector2(x4, max3.y);
		Vector3 center3 = this.col2d.bounds.center;
		float x5 = center3.x;
		Vector3 size2 = this.col2d.bounds.size;
		float x6 = x5 - size2.x / 4f;
		Vector3 max4 = this.col2d.bounds.max;
		Vector2 origin4 = new Vector2(x6, max4.y);
		Vector2 direction = new Vector2(-0.5f, 1f);
		Vector2 direction2 = new Vector2(0.5f, 1f);
		Vector2 up = Vector2.up;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin2, direction, 2f, 256);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin, direction2, 2f, 256);
		RaycastHit2D raycastHit2D3 = Physics2D.Raycast(origin3, up, 1f, 256);
		RaycastHit2D raycastHit2D4 = Physics2D.Raycast(origin4, up, 1f, 256);
		if (!((UnityEngine.Object)raycastHit2D.collider != (UnityEngine.Object)null) && !((UnityEngine.Object)raycastHit2D2.collider != (UnityEngine.Object)null) && !((UnityEngine.Object)raycastHit2D3.collider != (UnityEngine.Object)null) && !((UnityEngine.Object)raycastHit2D4.collider != (UnityEngine.Object)null))
		{
			return false;
		}
		return true;
	}

	public bool CheckTouchingGround()
	{
		Vector3 min = this.col2d.bounds.min;
		float x = min.x;
		Vector3 center = this.col2d.bounds.center;
		Vector2 origin = new Vector2(x, center.y);
		Vector2 origin2 = this.col2d.bounds.center;
		Vector3 max = this.col2d.bounds.max;
		float x2 = max.x;
		Vector3 center2 = this.col2d.bounds.center;
		Vector2 origin3 = new Vector2(x2, center2.y);
		Vector3 extents = this.col2d.bounds.extents;
		float distance = extents.y + 0.16f;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.down, distance, 256);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin2, Vector2.down, distance, 256);
		RaycastHit2D raycastHit2D3 = Physics2D.Raycast(origin3, Vector2.down, distance, 256);
		if (!((UnityEngine.Object)raycastHit2D.collider != (UnityEngine.Object)null) && !((UnityEngine.Object)raycastHit2D2.collider != (UnityEngine.Object)null) && !((UnityEngine.Object)raycastHit2D3.collider != (UnityEngine.Object)null))
		{
			return false;
		}
		return true;
	}

	private List<CollisionSide> CheckTouching(PhysLayers layer)
	{
		List<CollisionSide> list = new List<CollisionSide>(4);
		Vector3 center = this.col2d.bounds.center;
		Vector3 extents = this.col2d.bounds.extents;
		float distance = extents.x + 0.16f;
		Vector3 extents2 = this.col2d.bounds.extents;
		float distance2 = extents2.y + 0.16f;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(center, Vector2.up, distance2, 1 << (int)layer);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(center, Vector2.right, distance, 1 << (int)layer);
		RaycastHit2D raycastHit2D3 = Physics2D.Raycast(center, Vector2.down, distance2, 1 << (int)layer);
		RaycastHit2D raycastHit2D4 = Physics2D.Raycast(center, Vector2.left, distance, 1 << (int)layer);
		if ((UnityEngine.Object)raycastHit2D.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.top);
		}
		if ((UnityEngine.Object)raycastHit2D2.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.right);
		}
		if ((UnityEngine.Object)raycastHit2D3.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.bottom);
		}
		if ((UnityEngine.Object)raycastHit2D4.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.left);
		}
		return list;
	}

	private List<CollisionSide> CheckTouchingAdvanced(PhysLayers layer)
	{
		List<CollisionSide> list = new List<CollisionSide>();
		Vector3 min = this.col2d.bounds.min;
		float x = min.x;
		Vector3 max = this.col2d.bounds.max;
		Vector2 origin = new Vector2(x, max.y);
		Vector3 center = this.col2d.bounds.center;
		float x2 = center.x;
		Vector3 max2 = this.col2d.bounds.max;
		Vector2 origin2 = new Vector2(x2, max2.y);
		Vector3 max3 = this.col2d.bounds.max;
		float x3 = max3.x;
		Vector3 max4 = this.col2d.bounds.max;
		Vector2 origin3 = new Vector2(x3, max4.y);
		Vector3 min2 = this.col2d.bounds.min;
		float x4 = min2.x;
		Vector3 center2 = this.col2d.bounds.center;
		Vector2 origin4 = new Vector2(x4, center2.y);
		Vector3 max5 = this.col2d.bounds.max;
		float x5 = max5.x;
		Vector3 center3 = this.col2d.bounds.center;
		Vector2 origin5 = new Vector2(x5, center3.y);
		Vector3 min3 = this.col2d.bounds.min;
		float x6 = min3.x;
		Vector3 min4 = this.col2d.bounds.min;
		Vector2 origin6 = new Vector2(x6, min4.y);
		Vector3 center4 = this.col2d.bounds.center;
		float x7 = center4.x;
		Vector3 min5 = this.col2d.bounds.min;
		Vector2 origin7 = new Vector2(x7, min5.y);
		Vector3 max6 = this.col2d.bounds.max;
		float x8 = max6.x;
		Vector3 min6 = this.col2d.bounds.min;
		Vector2 origin8 = new Vector2(x8, min6.y);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.up, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin2, Vector2.up, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D3 = Physics2D.Raycast(origin3, Vector2.up, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D4 = Physics2D.Raycast(origin3, Vector2.right, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D5 = Physics2D.Raycast(origin5, Vector2.right, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D6 = Physics2D.Raycast(origin8, Vector2.right, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D7 = Physics2D.Raycast(origin8, Vector2.down, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D8 = Physics2D.Raycast(origin7, Vector2.down, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D9 = Physics2D.Raycast(origin6, Vector2.down, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D10 = Physics2D.Raycast(origin6, Vector2.left, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D11 = Physics2D.Raycast(origin4, Vector2.left, 0.16f, 1 << (int)layer);
		RaycastHit2D raycastHit2D12 = Physics2D.Raycast(origin, Vector2.left, 0.16f, 1 << (int)layer);
		if ((UnityEngine.Object)raycastHit2D.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D2.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D3.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.top);
		}
		if ((UnityEngine.Object)raycastHit2D4.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D5.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D6.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.right);
		}
		if ((UnityEngine.Object)raycastHit2D7.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D8.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D9.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.bottom);
		}
		if ((UnityEngine.Object)raycastHit2D10.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D11.collider != (UnityEngine.Object)null || (UnityEngine.Object)raycastHit2D12.collider != (UnityEngine.Object)null)
		{
			list.Add(CollisionSide.left);
		}
		return list;
	}

	private CollisionSide FindCollisionDirection(Collision2D collision)
	{
		ContactPoint2D contactPoint2D = collision.contacts[0];
		Vector2 normal = contactPoint2D.normal;
		float x = normal.x;
		Vector2 normal2 = contactPoint2D.normal;
		float y = normal2.y;
		if (y >= 0.5f)
		{
			return CollisionSide.bottom;
		}
		if (y <= -0.5f)
		{
			return CollisionSide.top;
		}
		if (x < 0f)
		{
			return CollisionSide.right;
		}
		if (x > 0f)
		{
			return CollisionSide.left;
		}
		object[] obj = new object[5]
		{
			"ERROR: unable to determine direction of collision - contact points at (",
			null,
			null,
			null,
			null
		};
		Vector2 normal3 = contactPoint2D.normal;
		obj[1] = normal3.x;
		obj[2] = ",";
		Vector2 normal4 = contactPoint2D.normal;
		obj[3] = normal4.y;
		obj[4] = ")";
		Debug.LogError(string.Concat(obj));
		return CollisionSide.bottom;
	}

	private bool CanJump()
	{
		if (this.hero_state != ActorStates.no_input && this.hero_state != ActorStates.hard_landing && this.hero_state != ActorStates.dash_landing && !this.cState.wallSliding && !this.cState.dashing && !this.cState.backDashing && !this.cState.jumping && !this.cState.bouncing && !this.cState.shroomBouncing)
		{
			if (this.cState.onGround)
			{
				return true;
			}
			if (this.ledgeBufferSteps > 0 && !this.cState.dead && !this.cState.hazardDeath && !this.controlReqlinquished && this.headBumpSteps <= 0 && !this.CheckNearRoof())
			{
				this.ledgeBufferSteps = 0;
				return true;
			}
			return false;
		}
		return false;
	}

	private bool CanDoubleJump()
	{
		if (this.playerData.hasDoubleJump && !this.doubleJumped && !this.inAcid && this.hero_state != ActorStates.no_input && this.hero_state != ActorStates.hard_landing && this.hero_state != ActorStates.dash_landing && !this.cState.dashing && !this.cState.wallSliding && !this.cState.backDashing && !this.cState.attacking && !this.cState.bouncing && !this.cState.shroomBouncing && !this.cState.onGround)
		{
			return true;
		}
		return false;
	}

	private bool CanInfiniteAirJump()
	{
		if (this.playerData.infiniteAirJump && this.hero_state != ActorStates.hard_landing && !this.cState.onGround)
		{
			return true;
		}
		return false;
	}

	private bool CanSwim()
	{
		if (this.hero_state != ActorStates.no_input && this.hero_state != ActorStates.hard_landing && this.hero_state != ActorStates.dash_landing && !this.cState.attacking && !this.cState.dashing && !this.cState.jumping && !this.cState.bouncing && !this.cState.shroomBouncing && !this.cState.onGround && this.inAcid)
		{
			return true;
		}
		return false;
	}

	private bool CanDash()
	{
		if (this.hero_state != ActorStates.no_input && this.hero_state != ActorStates.hard_landing && this.hero_state != ActorStates.dash_landing && this.dashCooldownTimer <= 0f && !this.cState.dashing && !this.cState.backDashing && (!this.cState.attacking || !(this.attack_time < this.ATTACK_RECOVERY_TIME)) && !this.cState.preventDash && (this.cState.onGround || !this.airDashed || this.cState.wallSliding) && this.playerData.canDash)
		{
			return true;
		}
		return false;
	}

	private bool CanAttack()
	{
		if (this.attack_cooldown <= 0f && !this.cState.attacking && !this.cState.dashing && !this.cState.dead && !this.cState.hazardDeath && !this.cState.hazardRespawning && !this.controlReqlinquished && this.hero_state != ActorStates.no_input && this.hero_state != ActorStates.hard_landing && this.hero_state != ActorStates.dash_landing)
		{
			return true;
		}
		return false;
	}

	private bool CanNailCharge()
	{
		if (!this.cState.attacking && !this.controlReqlinquished && !this.cState.recoiling && !this.cState.recoilingLeft && !this.cState.recoilingRight && this.playerData.hasNailArt)
		{
			return true;
		}
		return false;
	}

	private bool CanWallSlide()
	{
		if (this.cState.wallSliding && this.gm.isPaused)
		{
			return true;
		}
		if (!this.cState.touchingNonSlider && !this.inAcid && !this.cState.dashing && this.playerData.hasWalljump && !this.cState.onGround && !this.cState.recoiling && !this.gm.isPaused && !this.controlReqlinquished && !this.cState.transitioning && (this.cState.falling || this.cState.wallSliding) && !this.cState.doubleJumping && this.CanInput())
		{
			return true;
		}
		return false;
	}

	private bool CanTakeDamage()
	{
		if (this.damageMode != DamageMode.NO_DAMAGE && !this.cState.invulnerable && !this.cState.recoiling && !this.playerData.isInvincible && !this.cState.dead && !this.cState.hazardDeath)
		{
			return true;
		}
		return false;
	}

	private bool CanWallJump()
	{
		if (this.playerData.hasWalljump)
		{
			if (this.cState.touchingNonSlider)
			{
				return false;
			}
			if (this.cState.wallSliding)
			{
				return true;
			}
			if (this.cState.touchingWall && !this.cState.onGround)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	private bool ShouldHardLand(Collision2D collision)
	{
		if (!(bool)collision.gameObject.GetComponent<NoHardLanding>() && this.cState.willHardLand && !this.inAcid && this.hero_state != ActorStates.hard_landing)
		{
			return true;
		}
		return false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (this.cState.superDashing && (this.CheckStillTouchingWall(CollisionSide.left, false) || this.CheckStillTouchingWall(CollisionSide.right, false)))
		{
			this.superDash.SendEvent("HIT WALL");
		}
		if ((collision.gameObject.layer == 8 || collision.gameObject.CompareTag("HeroWalkable")) && this.CheckTouchingGround())
		{
			this.proxyFSM.SendEvent("HeroCtrl-Landed");
		}
		if (this.hero_state != ActorStates.no_input)
		{
			CollisionSide collisionSide = this.FindCollisionDirection(collision);
			if (collision.gameObject.layer != 8 && !collision.gameObject.CompareTag("HeroWalkable"))
			{
				return;
			}
			this.fallTrailGenerated = false;
			if (collisionSide == CollisionSide.top)
			{
				this.headBumpSteps = this.HEAD_BUMP_STEPS;
				if (this.cState.jumping)
				{
					this.CancelJump();
					this.CancelDoubleJump();
				}
				if (this.cState.bouncing)
				{
					this.CancelBounce();
					Rigidbody2D rigidbody2D = this.rb2d;
					Vector2 velocity = this.rb2d.velocity;
					rigidbody2D.velocity = new Vector2(velocity.x, 0f);
				}
				if (this.cState.shroomBouncing)
				{
					this.CancelBounce();
					Rigidbody2D rigidbody2D2 = this.rb2d;
					Vector2 velocity2 = this.rb2d.velocity;
					rigidbody2D2.velocity = new Vector2(velocity2.x, 0f);
				}
			}
			if (collisionSide == CollisionSide.bottom)
			{
				if (this.cState.attacking)
				{
					this.CancelDownAttack();
				}
				if (this.ShouldHardLand(collision))
				{
					this.DoHardLanding();
				}
				else if ((UnityEngine.Object)collision.gameObject.GetComponent<SteepSlope>() == (UnityEngine.Object)null && this.hero_state != ActorStates.hard_landing)
				{
					this.BackOnGround();
				}
				if (this.cState.dashing && this.dashingDown)
				{
					this.AffectedByGravity(true);
					this.SetState(ActorStates.dash_landing);
					this.hardLanded = true;
				}
			}
		}
		else if (this.hero_state == ActorStates.no_input && this.transitionState == HeroTransitionState.DROPPING_DOWN)
		{
			if (this.gatePosition != GatePosition.bottom && this.gatePosition != 0)
			{
				return;
			}
			this.FinishedEnteringScene(true);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (this.cState.superDashing && (this.CheckStillTouchingWall(CollisionSide.left, false) || this.CheckStillTouchingWall(CollisionSide.right, false)))
		{
			this.superDash.SendEvent("HIT WALL");
		}
		if (this.hero_state != ActorStates.no_input && collision.gameObject.layer == 8)
		{
			if ((UnityEngine.Object)collision.gameObject.GetComponent<NonSlider>() == (UnityEngine.Object)null)
			{
				this.cState.touchingNonSlider = false;
				if (this.CheckStillTouchingWall(CollisionSide.left, false))
				{
					this.cState.touchingWall = true;
					this.touchingWallL = true;
					this.touchingWallR = false;
				}
				else if (this.CheckStillTouchingWall(CollisionSide.right, false))
				{
					this.cState.touchingWall = true;
					this.touchingWallL = false;
					this.touchingWallR = true;
				}
				else
				{
					this.cState.touchingWall = false;
					this.touchingWallL = false;
					this.touchingWallR = false;
				}
				if (this.CheckTouchingGround())
				{
					if (this.ShouldHardLand(collision))
					{
						this.DoHardLanding();
					}
					else if (this.hero_state != ActorStates.hard_landing && this.hero_state != ActorStates.dash_landing && this.cState.falling)
					{
						this.BackOnGround();
					}
				}
				else
				{
					if (!this.cState.jumping && !this.cState.falling)
					{
						return;
					}
					this.cState.onGround = false;
					this.proxyFSM.SendEvent("HeroCtrl-LeftGround");
					this.SetState(ActorStates.airborne);
				}
			}
			else
			{
				this.cState.touchingNonSlider = true;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (this.cState.recoilingLeft || this.cState.recoilingRight)
		{
			this.cState.touchingWall = false;
			this.touchingWallL = false;
			this.touchingWallR = false;
			this.cState.touchingNonSlider = false;
		}
		if (this.hero_state != ActorStates.no_input && !this.cState.recoiling && collision.gameObject.layer == 8 && !this.CheckTouchingGround())
		{
			if (!this.cState.jumping && !this.fallTrailGenerated && this.cState.onGround)
			{
				if (this.playerData.environmentType != 6)
				{
					this.fsm_fallTrail.SendEvent("PLAY");
				}
				this.fallTrailGenerated = true;
			}
			this.cState.onGround = false;
			this.proxyFSM.SendEvent("HeroCtrl-LeftGround");
			this.SetState(ActorStates.airborne);
			if (this.cState.wasOnGround)
			{
				this.ledgeBufferSteps = this.LEDGE_BUFFER_STEPS;
			}
		}
	}

	private void SetupGameRefs()
	{
		if (this.cState == null)
		{
			this.cState = new HeroControllerStates();
		}
		this.gm = GameManager.instance;
		this.animCtrl = base.GetComponent<HeroAnimationController>();
		this.rb2d = base.GetComponent<Rigidbody2D>();
		this.col2d = base.GetComponent<Collider2D>();
		this.transform = base.GetComponent<Transform>();
		this.renderer = base.GetComponent<MeshRenderer>();
		this.audioCtrl = base.GetComponent<HeroAudioController>();
		this.inputHandler = ((Component)this.gm).GetComponent<InputHandler>();
		this.proxyFSM = FSMUtility.LocateFSM(base.gameObject, "ProxyFSM");
		this.audioSource = base.GetComponent<AudioSource>();
		if (!(bool)this.footStepsRunAudioSource)
		{
			this.footStepsRunAudioSource = ((Component)this.transform.Find("Sounds/FootstepsRun")).GetComponent<AudioSource>();
		}
		if (!(bool)this.footStepsWalkAudioSource)
		{
			this.footStepsWalkAudioSource = ((Component)this.transform.Find("Sounds/FootstepsWalk")).GetComponent<AudioSource>();
		}
		this.invPulse = base.GetComponent<InvulnerablePulse>();
		this.spriteFlash = base.GetComponent<SpriteFlash>();
		this.gm.UnloadingLevel += this.OnLevelUnload;
		this.prevGravityScale = this.DEFAULT_GRAVITY;
		this.transition_vel = Vector2.zero;
		this.current_velocity = Vector2.zero;
		this.acceptingInput = true;
		this.positionHistory = new Vector2[2];
	}

	private void SetupPools()
	{
		this.runEffectPrefab.CreatePool(15);
		this.softLandingEffectPrefab.CreatePool(10);
		this.hardLandingEffectPrefab.CreatePool(10);
		this.fallEffectPrefab.CreatePool(10);
		this.jumpEffectPrefab.CreatePool(12);
		this.jumpTrailPrefab.CreatePool(12);
		this.backDashPrefab.CreatePool(10);
		this.nailTerrainImpactEffectPrefab.CreatePool(3);
		this.corpsePrefab.CreatePool(1);
	}

	private void FilterInput()
	{
		if (this.move_input > 0.3f)
		{
			this.move_input = 1f;
		}
		else if (this.move_input < -0.3f)
		{
			this.move_input = -1f;
		}
		else
		{
			this.move_input = 0f;
		}
		if (this.vertical_input > 0.5f)
		{
			this.vertical_input = 1f;
		}
		else if (this.vertical_input < -0.5f)
		{
			this.vertical_input = -1f;
		}
		else
		{
			this.vertical_input = 0f;
		}
	}

	public Vector3 FindGroundPoint(Vector2 startPoint, bool useExtended = false)
	{
		float num = this.FIND_GROUND_POINT_DISTANCE;
		if (useExtended)
		{
			num = this.FIND_GROUND_POINT_DISTANCE_EXT;
		}
		RaycastHit2D raycastHit2D = Physics2D.Raycast(startPoint, Vector2.down, num, 256);
		if ((UnityEngine.Object)raycastHit2D.collider == (UnityEngine.Object)null)
		{
			Debug.LogErrorFormat("FindGroundPoint: Could not find ground point below {0}, check reference position is not too high (more than {1} tiles).", startPoint.ToString(), num);
		}
		Vector2 point = raycastHit2D.point;
		float x = point.x;
		Vector2 point2 = raycastHit2D.point;
		float y = point2.y;
		Vector3 extents = this.col2d.bounds.extents;
		float num2 = y + extents.y;
		Vector2 offset = this.col2d.offset;
		float y2 = num2 - offset.y + 0.01f;
		Vector3 position = this.transform.position;
		return new Vector3(x, y2, position.z);
	}

	private float FindGroundPointY(float x, float y, bool useExtended = false)
	{
		float num = this.FIND_GROUND_POINT_DISTANCE;
		if (useExtended)
		{
			num = this.FIND_GROUND_POINT_DISTANCE_EXT;
		}
		RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(x, y), Vector2.down, num, 256);
		if ((UnityEngine.Object)raycastHit2D.collider == (UnityEngine.Object)null)
		{
			Debug.LogErrorFormat("FindGroundPoint: Could not find ground point below ({0},{1}), check reference position is not too high (more than {2} tiles).", x, y, num);
		}
		Vector2 point = raycastHit2D.point;
		float y2 = point.y;
		Vector3 extents = this.col2d.bounds.extents;
		float num2 = y2 + extents.y;
		Vector2 offset = this.col2d.offset;
		return num2 - offset.y + 0.01f;
	}
}
