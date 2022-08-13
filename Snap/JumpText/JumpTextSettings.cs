using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CYM.UI.JumpText
{
    [System.Serializable]
    public struct CombinationSettings
    {
        public CombinationSettings(float customDefault)
        {
            absorberType = AbsorberType.OLDEST;
            combinationGroup = "";
            maxDistance = 10f;

            bonusLifetime = 1f;
            delay = 0.2f;

            absorberSpeed = 0;
            targetSpeed = 5f;

            absorbTime = 0.4f;
            targetFadeOutSpeed = 1f;
            targetScaleDownSpeed = 1f;

            instantGain = false;

            absorberScaleGain = new Vector2(1.5f, 1.5f);
            absorberScaleFade = 15;
        }

        [Header("Main:")]
        [Tooltip("If set to oldest, the oldest number will absorb new numbers.  If set to newest, the newest number will absorb existing numbers.")]
        public AbsorberType absorberType;
        [Tooltip("Only numbers of the same combination group will combine with each other.")]
        public string combinationGroup;
        [Tooltip("The maximum distance at which numbers will combine.")]
        public float maxDistance;
        [Tooltip("If true, the absorber will instantly gain the numbers of the target.  Should be used when combination is very fast.")]
        public bool instantGain;
        [Tooltip("Delay after spawning that a number can be absorbed.")]
        public float delay;

        [Header("Lifetime:")]
        [Tooltip("The lifetime of the absorber is reset and also increased by this bonus lifetime.")]
        public float bonusLifetime;

        [Header("Movement:")]
        [Tooltip("Speed of the absorber while absorbing.")]
        public float absorberSpeed;
        [Tooltip("Speed of the target while being absorbed.")]
        public float targetSpeed;
        [Tooltip("Time at which the target is absorbed and destroyed.")]
        public float absorbTime;

        [Header("Fading:")]
        [Tooltip("Fades out the target while it is being absorbed.  Keep this at 1 to 100% fade it out by the time it is absorbed.")]
        public float targetFadeOutSpeed;
        [Tooltip("Scales down the target while it is being absorbed.")]
        public float targetScaleDownSpeed;

        [Header("Scale:")]
        [Tooltip("Absorber gains a scale up when it absorbs the target.")]
        public Vector2 absorberScaleGain;
        [Tooltip("Speed at which the scale up fades out.")]
        public float absorberScaleFade;
    }

    [System.Serializable]
    public enum AbsorberType
    {
        OLDEST
        ,
        NEWEST
    }

    [System.Serializable]
    public struct DigitSettings
    {
        public DigitSettings(float customDefault)
        {
            decimals = 0;
            decimalChar = ".";
            hideZeros = false;

            dotSeperation = false;
            dotDistance = 3;
            dotChar = ".";

            suffixShorten = false;
            suffixes = new List<string>() { "K", "M", "B" };
            suffixDigits = new List<int>() { 3, 3, 3 };
            maxDigits = 4;
        }

        [Header("Decimals:")]
        [Range(0, 3)]
        [Tooltip("Amount of digits visible after the dot.")]
        public int decimals;
        [Tooltip("The character used for the dot.")]
        public string decimalChar;
        [Tooltip("If true zeros at the end of the number will be hidden.")]
        public bool hideZeros;

        [Header("Dots:")]
        [Tooltip("Seperates the number with dots.")]
        public bool dotSeperation;
        [Tooltip("Amount of digits between each dot.")]
        public int dotDistance;
        [Tooltip("The character used for the dot.")]
        public string dotChar;

        [Header("Suffix Shorten:")]
        [Tooltip("Shortens a number like 10000 to 10K.")]
        public bool suffixShorten;
        [Tooltip("List of suffixes.")]
        public List<string> suffixes;
        [Tooltip("Corresponding list of how many digits a suffix shortens.  Keep both lists at the same size.")]
        public List<int> suffixDigits;
        [Tooltip("Maximum of visible digits.  If number has more digits than this it will be shortened.")]
        public int maxDigits;
    }

    [System.Serializable]
    public struct FadeSettings
    {
        public FadeSettings(Vector2 newScale)
        {
            fadeDuration = 0.2f;

            positionOffset = new Vector2(0.5f, 0);
            scaleOffset = Vector2.one;
            scale = newScale;
        }

        [Tooltip("The duration that the fade takes.")]
        public float fadeDuration;
        [Space(8)]
        [Tooltip("Moves TextA and TextB in opposite directions based on this offset.")]
        public Vector2 positionOffset;
        [Tooltip("Scales TextA and divides the scale of TextB by this scale.")]
        public Vector2 scaleOffset;
        [Tooltip("Scales both TextA and TextB by this scale.")]
        public Vector2 scale;
    }
    [System.Serializable]
    public struct LerpSettings
    {
        public LerpSettings(float customDefault)
        {
            minX = -0.7f;
            maxX = 0.7f;
            minY = 1f;
            maxY = 2f;

            speed = 5f;
        }

        [Header("Offset:")]
        [Tooltip("Minimum of horizontal offset.")]
        public float minX;
        [Tooltip("Maximum of horizontal offset.")]
        public float maxX;
        [Tooltip("Minimum of vertical offset.")]
        public float minY;
        [Tooltip("Maximum of vertical offset.")]
        public float maxY;

        [Header("Speed:")]
        [Tooltip("Speed at which it moves to the offset position.")]
        public float speed;
    }
    [System.Serializable]
    public enum MoveType
    {
        LERP
    ,
        VELOCITY
    }
    [System.Serializable]
    public struct PerspectiveSettings
    {
        public PerspectiveSettings(float customDefault)
        {
            baseDistance = 10f;
            closeDistance = 5f;
            farDistance = 50f;

            closeScale = 2f;
            farScale = 0.5f;
        }

        [Header("Distances:")]
        [Tooltip("The consistent size of the number is based on this distance.")]
        public float baseDistance;
        [Tooltip("The closest distance the number will be scaling up to.")]
        public float closeDistance;
        [Tooltip("The farthest distance the number will be scaling down to.")]
        public float farDistance;

        [Header("Scales:")]
        [Tooltip("The max scale the number reaches at the closest distance.")]
        public float closeScale;
        [Tooltip("The min scale the number reaches at the farthest distance.")]
        public float farScale;
    }
    [System.Serializable]
    public struct ShakeSettings
    {
        public ShakeSettings(Vector2 customDefault)
        {
            offset = customDefault;
            frequency = 50f;
        }

        [Tooltip("Moves back and fourth from negative offset to positive offset.")]
        public Vector2 offset;
        [Tooltip("Changes the speed at which the number moves back and fourth.\nUsed in a sinus function.")]
        public float frequency;
    }
    [System.Serializable]
    public struct TextSettings
    {
        public TextSettings(float customDefault)
        {
            horizontal = customDefault;

            customColor = false;
            color = new Color(1, 1, 0f, 1);

            size = 0;
            vertical = 0;
            characterSpacing = 0f;

            alpha = 1;

            mark = false;
            markColor = new Color(0, 0, 0, 0.5f);

            bold = false;
            italic = false;
            underline = false;
            strike = false;
            newLine = false;
        }

        [Header("Basics:")]
        [Tooltip("Makes the text bold.")]
        public bool bold;
        [Tooltip("Makes the text italic.")]
        public bool italic;
        [Tooltip("Adds an underline to the text.")]
        public bool underline;
        [Tooltip("Strikes through the text with a line.")]
        public bool strike;

        [Header("Alpha:")]
        [Range(0, 1)]
        [Tooltip("Changes the alpha of the text.\nWon't work if Custom Color is used.")]
        public float alpha;

        [Header("Color:")]
        [Tooltip("Changes the color of the text.\nOverrides the alpha option above.")]
        public bool customColor;
        public Color color;

        [Header("Mark:")]
        [Tooltip("Highlights the text with a custom color.")]
        public bool mark;
        public Color markColor;

        [Header("Offset:")]
        [Tooltip("Horizontally moves the text.\nCan be used to offset the prefix or suffix.")]
        public float horizontal;
        [Tooltip("Vertically moves the text.\nCan be used to offset the prefix or suffix.")]
        public float vertical;

        [Header("Extra:")]
        [Tooltip("Changes the character spacing.")]
        public float characterSpacing;
        [Tooltip("Changes the font size.")]
        public float size;
        [Tooltip("Displays this part of the text above the next part")]
        public bool newLine;
    }
    [System.Serializable]
    public struct VelocitySettings
    {
        public VelocitySettings(float customDefault)
        {
            minX = -1f;
            maxX = 1f;
            minY = 4f;
            maxY = 5f;

            dragX = 0.1f;
            dragY = 1f;
            gravity = 3f;
        }

        [Header("Velocity:")]
        [Tooltip("Minimum of horizontal velocity.")]
        public float minX;
        [Tooltip("Maximum of horizontal velocity.")]
        public float maxX;
        [Tooltip("Minimum of vertical velocity.")]
        public float minY;
        [Tooltip("Maximum of vertical velocity.")]
        public float maxY;

        [Header("Drag:")]
        [Tooltip("Reduces horizontal velocity over time.")]
        public float dragX;
        [Tooltip("Reduces vertical velocity over time.")]
        public float dragY;

        [Header("Gravity:")]
        [Tooltip("Increases vertical velocity downwards.")]
        public float gravity;
    }
}