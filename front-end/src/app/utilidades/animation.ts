import { AnimationReferenceMetadata, AnimationTriggerMetadata, animate, animation, keyframes, state, style, transition, trigger, useAnimation } from "@angular/animations";

const DEFAULT_TIMING = 2000;

export function FadeIn(timingIn: number, height: boolean = false): AnimationTriggerMetadata  {
    return trigger('fadeIn', [
        transition(':enter', [
        style(height ? { opacity: 0 , height: 0, } : { opacity: 0, }),
        animate(timingIn, style(height ? { opacity: 1, height: 'fit-content' } : { opacity: 1, })),
        ]),
    ]);
}

export function FadeInOff(timingIn: number, height: boolean = false): AnimationTriggerMetadata  {
  return trigger('fadeInOff', [
      state('true',style(height ? { opacity: 1 , height: 0, } : { opacity: 1, }), ),
      state('false',style ({ opacity: 0 })),
      transition('* <=> *', [
      animate(timingIn, style(height ? { opacity: 1, height: 'fit-content' } : { opacity: 1, })),
      ]),
  ]);
}

export function BounceAnimation(){
    return trigger('bounce',
    [
        transition(':enter', [
            style({ transform: 'translate3d(0, 0, 0)' }),
            animate(
            DEFAULT_TIMING,
            keyframes([
                style({ transform: 'translate3d(0, 0, 0)', offset: 0.2 }),
                style({ transform: 'translate3d(0, -30px, 0)', offset: 0.4 }),
                style({ transform: 'translate3d(0, 0, 0)', offset: 0.53 }),
                style({ transform: 'translate3d(0, -15px, 0)', offset: 0.7 }),
                style({ transform: 'translate3d(0, -4px, 0)', offset: 0.9 }),
                style({ transform: 'translate3d(0, 0, 0)', offset: 1 }),
            ])
            ),
        ])
    ]);
}

export function flashAnimation(){
    return trigger('flash',
    [
        transition(':enter', [
            animate(
            DEFAULT_TIMING,
            keyframes([
                style({ opacity: 1 }),
                style({ opacity: 0 }),
                style({ opacity: 1 }),
                style({ opacity: 0 }),
                style({ opacity: 1 }),
            ])
            ),
        ])
    ]);
}

export function PulseAnimation(){
    return trigger('pulse',
    [
        transition(':enter', [
            animate(
            DEFAULT_TIMING,
            keyframes([
                style({ transform: 'scale3d(1, 1, 1)' }),
                style({ transform: 'scale3d(1.25, 1.25, 1.25)' }),
                style({ transform: 'scale3d(1, 1, 1)' }),
              ])
            ),
        ])
    ]);
}

export function RubberAnimation(){
    return trigger('rubber',
    [
        transition(':enter', [
            animate(
            DEFAULT_TIMING,
            keyframes([
                style({ transform: 'scale3d(1, 1, 1)', offset: 0 }),
                style({ transform: 'scale3d(1.25, 0.75, 1)', offset: 0.3 }),
                style({ transform: 'scale3d(0.75, 1.25, 1)', offset: 0.4 }),
                style({ transform: 'scale3d(1.15, 0.85, 1)', offset: 0.5 }),
                style({ transform: 'scale3d(.95, 1.05, 1)', offset: 0.65 }),
                style({ transform: 'scale3d(1.05, .95, 1)', offset: 0.75 }),
                style({ transform: 'scale3d(1, 1, 1)', offset: 1 }),
            ])
            ),
        ])
    ]);
}
  
export function ShakeAnimation(){
    return trigger('shake',
    [
        transition(':enter', [
            animate(
            DEFAULT_TIMING,
            keyframes([
                style({ transform: 'translate3d(0, 0, 0)', offset: 0 }),
                style({ transform: 'translate3d(10px, 0, 0)', offset: 0.1 }),
                style({ transform: 'translate3d(-10px, 0, 0)', offset: 0.2 }),
                style({ transform: 'translate3d(10px, 0, 0)', offset: 0.3 }),
                style({ transform: 'translate3d(-10px, 0, 0)', offset: 0.4 }),
                style({ transform: 'translate3d(10px, 0, 0)', offset: 0.5 }),
                style({ transform: 'translate3d(-10px, 0, 0)', offset: 0.6 }),
                style({ transform: 'translate3d(10px, 0, 0)', offset: 0.7 }),
                style({ transform: 'translate3d(-10px, 0, 0)', offset: 0.8 }),
                style({ transform: 'translate3d(10px, 0, 0)', offset: 0.9 }),
                style({ transform: 'translate3d(0, 0, 0)', offset: 1 }),
              ])
            ),
        ])
    ]);
}

export function SwingAnimation(){
    return trigger('swing',
    [
        transition(':enter', [
            animate(
            DEFAULT_TIMING,
            keyframes([
                style({ transform: 'rotate3d(0, 0, 1, 15deg)', offset: 0.2 }),
                style({ transform: 'rotate3d(0, 0, 1, -10deg)', offset: 0.4 }),
                style({ transform: 'rotate3d(0, 0, 1, 5deg)', offset: 0.6 }),
                style({ transform: 'rotate3d(0, 0, 1, -5deg)', offset: 0.8 }),
                style({ transform: 'rotate3d(0, 0, 1, 0deg)', offset: 1 }),
              ])
            ),
        ])
    ]);
}
  
  export const tada = animation(
    animate(
      '{{ timing }}s {{ delay }}s',
      keyframes([
        style({ transform: 'scale3d(1, 1, 1)', offset: 0 }),
        style({
          transform: 'scale3d(.9, .9, .9) rotate3d(0, 0, 1, -3deg)',
          offset: 0.1,
        }),
        style({
          transform: 'scale3d(.9, .9, .9) rotate3d(0, 0, 1, -3deg)',
          offset: 0.2,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg)',
          offset: 0.3,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg)',
          offset: 0.4,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg)',
          offset: 0.5,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg)',
          offset: 0.6,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg)',
          offset: 0.7,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg)',
          offset: 0.8,
        }),
        style({
          transform: 'scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg)',
          offset: 0.9,
        }),
        style({ transform: 'scale3d(1, 1, 1)', offset: 1 }),
      ])
    ),
    { params: { timing: DEFAULT_TIMING, delay: 0 } }
  );
  
  export const wobble = animation(
    animate(
      '{{ timing }}s {{ delay }}s',
      keyframes([
        style({ transform: 'none', offset: 0 }),
        style({
          transform: 'translate3d(-25%, 0, 0) rotate3d(0, 0, 1, -5deg)',
          offset: 0.15,
        }),
        style({
          transform: 'translate3d(20%, 0, 0) rotate3d(0, 0, 1, 3deg)',
          offset: 0.3,
        }),
        style({
          transform: 'translate3d(-15%, 0, 0) rotate3d(0, 0, 1, -3deg)',
          offset: 0.45,
        }),
        style({
          transform: 'translate3d(10%, 0, 0) rotate3d(0, 0, 1, 2deg)',
          offset: 0.6,
        }),
        style({
          transform: 'translate3d(-5%, 0, 0) rotate3d(0, 0, 1, -1deg)',
          offset: 0.75,
        }),
        style({ transform: 'none', offset: 1 }),
      ])
    ),
    { params: { timing: DEFAULT_TIMING, delay: 0 } }
  );
  
  export const jello = animation(
    animate(
      '{{ timing }}s {{ delay }}s',
      keyframes([
        style({ transform: 'none', offset: 0 }),
        style({ transform: 'none', offset: 0.11 }),
        style({ transform: 'skewX(-12.5deg) skewY(-12.5deg)', offset: 0.22 }),
        style({ transform: 'skewX(6.25deg) skewY(6.25deg)', offset: 0.33 }),
        style({ transform: 'skewX(-3.125deg) skewY(-3.125deg)', offset: 0.44 }),
        style({ transform: 'skewX(1.5625deg) skewY(1.5625deg)', offset: 0.55 }),
        style({
          transform: 'skewX(-0.78125deg) skewY(-0.78125deg)',
          offset: 0.66,
        }),
        style({
          transform: 'skewX(0.390625deg) skewY(0.390625deg)',
          offset: 0.77,
        }),
        style({
          transform: 'skewX(-0.1953125deg) skewY(-0.1953125deg)',
          offset: 0.88,
        }),
        style({ transform: 'none', offset: 1 }),
      ])
    ),
    { params: { timing: DEFAULT_TIMING, delay: 0 } }
  );
  
  export const heartBeat = animation(
    animate(
      '{{ timing }}s {{ delay }}s ease-in-out',
      keyframes([
        style({ transform: 'scale(1)', offset: 0 }),
        style({ transform: 'scale({{ scale }})', offset: 0.14 }),
        style({ transform: 'scale(1)', offset: 0.28 }),
        style({
          transform: 'scale({{ scale }})',
          offset: 0.42,
        }),
        style({
          transform: 'scale(1)',
          offset: 0.7,
        }),
      ])
    ),
    { params: { timing: DEFAULT_TIMING * 1.3, scale: 1.3, delay: 0 } }
  );
  
  export const headShake = animation(
    animate(
      '{{ timing }}s {{ delay }}s ease-in-out',
      keyframes([
        style({ transform: 'translateX(0)', offset: 0 }),
        style({ transform: 'translateX(-6px) rotateY(-9deg)', offset: 0.065 }),
        style({ transform: 'translateX(5px) rotateY(7deg)', offset: 0.185 }),
        style({ transform: 'translateX(-3px) rotateY(-5deg)', offset: 0.315 }),
        style({ transform: 'translateX(2px) rotateY(3deg)', offset: 0.435 }),
        style({ transform: 'translateX(0)', offset: 0.5 }),
      ])
    ),
    { params: { timing: DEFAULT_TIMING, delay: 0 } }
  );

  export function fadeXY(
    fromX: string | 0,
    fromY: string | 0,
    toX: string | 0,
    toY: string | 0,
    fromOpacity = 0,
    toOpacity = 1
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            opacity: '{{ fromOpacity }}',
            transform: 'translate3d({{ fromX }}, {{ fromY }}, 0)',
            offset: 0,
          }),
          style({
            opacity: '{{ toOpacity }}',
            transform: 'translate3d({{ toX }}, {{ toY }}, 0)',
            offset: 1,
          }),
        ])
      ),
      {
        params: {
          timing: DEFAULT_TIMING,
          delay: 0,
          fromX,
          toX,
          fromY,
          toY,
          fromOpacity,
          toOpacity,
        },
      }
    );
  }
  
  export function fadeInX(
    a: string | 0,
    b: string | 0,
    fromOpacity = 0,
    toOpacity = 1
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            opacity: '{{ fromOpacity }}',
            transform: 'translate3d({{ a }}, 0, 0)',
            offset: 0,
          }),
          style({
            opacity: '{{ toOpacity }}',
            transform: 'translate3d({{ b }}, 0, 0)',
            offset: 1,
          }),
        ])
      ),
      {
        params: {
          timing: DEFAULT_TIMING,
          delay: 0,
          a,
          b,
          fromOpacity,
          toOpacity,
        },
      }
    );
  }
  
  export function fadeInY(
    a: string | 0,
    b: string | 0,
    fromOpacity = 0,
    toOpacity = 1
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            opacity: '{{ fromOpacity }}',
            transform: 'translate3d(0, {{ a }}, 0)',
            offset: 0,
          }),
          style({
            opacity: '{{ toOpacity }}',
            transform: 'translate3d(0, {{ b }}, 0)',
            offset: 1,
          }),
        ])
      ),
      {
        params: {
          timing: DEFAULT_TIMING,
          delay: 0,
          a,
          b,
          fromOpacity,
          toOpacity,
        },
      }
    );
  }
  
  export const fadeIn = fadeInX(0, 0);
  export const fadeInDown = fadeInY('-100%', 0);
  export const fadeInDownBig = fadeInY('-2000px', 0);
  export const fadeInUp = fadeInY('100%', 0);
  export const fadeInUpBig = fadeInY('2000px', 0);
  export const fadeInLeft = fadeInX('-100%', 0);
  export const fadeInLeftBig = fadeInX('-2000px', 0);
  export const fadeInRight = fadeInX('100%', 0);
  export const fadeInRightBig = fadeInX('2000px', 0);
  
  export const fadeInTopLeft = fadeXY('-100%', '-100%', 0, 0);
  export const fadeInTopRight = fadeXY('100%', '-100%', 0, 0);
  export const fadeInBottomLeft = fadeXY('-100%', '100%', 0, 0);
  export const fadeInBottomRight = fadeXY('100%', '100%', 0, 0);
  
  export function fadeOutX(
    a: string | 0,
    b: string | 0
  ): AnimationReferenceMetadata {
    return fadeInX(a, b, 1, 0);
  }
  
  export function fadeOutY(
    a: string | 0,
    b: string | 0
  ): AnimationReferenceMetadata {
    return fadeInY(a, b, 1, 0);
  }
  
  export const fadeOut = fadeOutX(0, 0);
  export const fadeOutDown = fadeOutY(0, '100%');
  export const fadeOutDownBig = fadeOutY(0, '2000px');
  export const fadeOutUp = fadeOutY(0, '-100%');
  export const fadeOutUpBig = fadeOutY(0, '-2000px');
  export const fadeOutLeft = fadeOutX(0, '-100%');
  export const fadeOutLeftBig = fadeOutX(0, '-2000px');
  export const fadeOutRight = fadeOutX(0, '100%');
  export const fadeOutRightBig = fadeOutX(0, '2000px');
  
  export const fadeOutTopLeft = fadeXY(0, 0, '-100%', '-100%', 1, 0);
  export const fadeOutTopRight = fadeXY(0, 0, '100%', '-100%', 1, 0);
  export const fadeOutBottomLeft = fadeXY(0, 0, '-100%', '100%', 1, 0);
  export const fadeOutBottomRight = fadeXY(0, 0, '100%', '100%', 1, 0);
  
  export function slideX(
    a: string | 0,
    b: string | 0
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            transform: 'translate3d({{ a }}, 0, 0)',
            offset: 0,
          }),
          style({
            transform: 'translate3d({{ b }}, 0, 0)',
            offset: 1,
          }),
        ])
      ),
      { params: { timing: DEFAULT_TIMING, delay: 0, a, b } }
    );
  }
  
  export function slideY(
    a: string | 0,
    b: string | 0
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            transform: 'translate3d(0, {{ a }}, 0)',
            offset: 0,
          }),
          style({
            transform: 'translate3d(0, {{ b }}, 0)',
            offset: 1,
          }),
        ])
      ),
      { params: { timing: DEFAULT_TIMING, delay: 0, a, b } }
    );
  }
  
  export const slideInUp = slideY('-100%', 0);
  export const slideInDown = slideY('100%', 0);
  export const slideInLeft = slideX('-100%', 0);
  export const slideInRight = slideX('100%', 0);
  export const slideOutUp = slideY(0, '-100%');
  export const slideOutDown = slideY(0, '100%');
  export const slideOutLeft = slideX(0, '-100%');
  export const slideOutRight = slideX(0, '100%');

  export function rotateInDirection(
    origin: string,
    degrees: string
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            'transform-origin': '{{ origin }}',
            opacity: '{{ fromOpacity }}',
            transform: 'rotate3d(0, 0, 1, {{ degrees }})',
            offset: 0,
          }),
          style({
            'transform-origin': '{{ origin }}',
            opacity: '{{ toOpacity }}',
            transform: 'none',
            offset: 1,
          }),
        ])
      ),
      {
        params: {
          timing: DEFAULT_TIMING,
          delay: 0,
          origin,
          degrees,
          fromOpacity: 0,
          toOpacity: 1,
        },
      }
    );
  }
  
  export function rotateOutDirection(
    origin: string,
    degrees: string
  ): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            'transform-origin': '{{ origin }}',
            opacity: '{{ fromOpacity }}',
            transform: 'none',
            offset: 0,
          }),
          style({
            'transform-origin': '{{ origin }}',
            opacity: '{{ toOpacity }}',
            transform: 'rotate3d(0, 0, 1, {{ degrees }})',
            offset: 1,
          }),
        ])
      ),
      {
        params: {
          timing: DEFAULT_TIMING,
          delay: 0,
          origin,
          degrees,
          fromOpacity: 1,
          toOpacity: 0,
        },
      }
    );
  }
  
  export const rotateIn = rotateInDirection('center', '-200deg');
  export const rotateInDownLeft = rotateInDirection('left bottom', '-45deg');
  export const rotateInDownRight = rotateInDirection('right bottom', '45deg');
  export const rotateInUpLeft = rotateInDirection('left bottom', '45deg');
  export const rotateInUpRight = rotateInDirection('right bottom', '-90deg');
  
  export const rotateOut = rotateOutDirection('center', '200deg');
  export const rotateOutDownLeft = rotateOutDirection('left bottom', '45deg');
  export const rotateOutDownRight = rotateOutDirection('right bottom', '-45deg');
  export const rotateOutUpLeft = rotateOutDirection('left bottom', '-45deg');
  export const rotateOutUpRight = rotateOutDirection('right bottom', '90deg');

  export const lightSpeedInLeft = animation(
    animate(
      '{{ timing }}s {{ delay }}s ease-out',
      keyframes([
        style({
          transform: 'translate3d(-100%, 0, 0) skewX(30deg)',
          opacity: 0,
          offset: 0,
        }),
        style({
          transform: 'skewX(-20deg)',
          opacity: 1,
          offset: 0.6,
        }),
        style({
          transform: 'skewX(5deg)',
          offset: 0.8,
        }),
        style({
          transform: 'translate3d(0, 0, 0)',
          offset: 1,
        }),
      ])
    ),
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );
  
  export const lightSpeedIn = animation(
    animate(
      '{{ timing }}s {{ delay }}s ease-out',
      keyframes([
        style({
          transform: 'translate3d(100%, 0, 0) skewX(-30deg)',
          opacity: 0,
          offset: 0,
        }),
        style({
          transform: 'skewX(20deg)',
          opacity: 1,
          offset: 0.6,
        }),
        style({
          transform: 'skewX(-5deg)',
          offset: 0.8,
        }),
        style({
          transform: 'translate3d(0, 0, 0)',
          offset: 1,
        }),
      ])
    ),
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );
  export const lightSpeedInRight = lightSpeedIn;
  
  export const lightSpeedOut = animation(
    animate(
      '{{ timing }}s {{ delay }}s ease-in',
      keyframes([
        style({
          opacity: 1,
          offset: 0,
        }),
        style({
          opacity: 0,
          transform: 'translate3d(100%, 0, 0) skewX(30deg)',
          offset: 1,
        }),
      ])
    ),
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );
  
  export const lightSpeedOutRight = lightSpeedOut;
  
  export const lightSpeedOutLeft = animation(
    animate(
      '{{ timing }}s {{ delay }}s ease-in',
      keyframes([
        style({
          opacity: 1,
          offset: 0,
        }),
        style({
          opacity: 0,
          transform: 'translate3d(-100%, 0, 0) skewX(-30deg)',
          offset: 1,
        }),
      ])
    ),
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );

  export const flip = animation(
    [
      style({ 'backface-visibility': 'visible' }),
      animate(
        '{{ timing }}s {{ delay }}s ease-out',
        keyframes([
          style({
            transform: 'perspective(400px) rotate3d(0, 1, 0, -360deg)',
            offset: 0,
          }),
          style({
            transform:
              'perspective(400px) scale3d(1.5, 1.5, 1.5) rotate3d(0, 1, 0, -190deg)',
            offset: 0.4,
          }),
          style({
            transform:
              'perspective(400px) scale3d(1.5, 1.5, 1.5) rotate3d(0, 1, 0, -170deg)',
            offset: 0.5,
          }),
          style({
            transform: 'perspective(400px) scale3d(.95, .95, .95)',
            offset: 0.8,
          }),
          style({
            transform: 'perspective(400px)',
            offset: 1,
          }),
        ])
      ),
    ],
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );
  
  export function flipIn(
    rotateX: number,
    rotateY: number
  ): AnimationReferenceMetadata {
    return animation(
      [
        style({ 'backface-visibility': 'visible' }),
        animate(
          '{{ timing }}s {{ delay }}s ease-in',
          keyframes([
            style({
              opacity: 0,
              transform:
                'perspective(400px) rotate3d({{ rotateX }}, {{ rotateY }}, 0, 90deg)',
              offset: 0,
            }),
            style({
              opacity: 1,
              transform:
                'perspective(400px) rotate3d({{ rotateX }}, {{ rotateY }}, 0, -20deg)',
              offset: 0.4,
            }),
            style({
              transform:
                'perspective(400px) rotate3d({{ rotateX }}, {{ rotateY }}, 0, 10deg)',
              offset: 0.6,
            }),
            style({
              transform:
                'perspective(400px) rotate3d({{ rotateX }}, {{ rotateY }}, 0, -5deg)',
              offset: 0.8,
            }),
            style({
              transform: 'perspective(400px) rotate3d(0, 0, 0, 0)',
              offset: 1,
            }),
          ])
        ),
      ],
      { params: { timing: DEFAULT_TIMING, delay: 0, rotateX, rotateY } }
    );
  }
  
  export const flipInX = flipIn(1, 0);
  export const flipInY = flipIn(0, 1);
  
  export function flipOut(
    rotateX: number,
    rotateY: number
  ): AnimationReferenceMetadata {
    return animation(
      [
        style({ 'backface-visibility': 'visible' }),
        animate(
          '{{ timing }}s {{ delay }}s',
          keyframes([
            style({
              transform: 'perspective(400px)',
              offset: 0,
            }),
            style({
              opacity: 1,
              transform:
                'perspective(400px) rotate3d({{ rotateX }}, {{ rotateY }}, 0, -20deg)',
              offset: 0.3,
            }),
            style({
              opacity: 0,
              transform:
                'perspective(400px) rotate3d({{ rotateX }}, {{ rotateY }}, 0, 90deg)',
              offset: 1,
            }),
          ])
        ),
      ],
      { params: { timing: DEFAULT_TIMING, delay: 0, rotateX, rotateY } }
    );
  }
  
  export const flipOutX = flipOut(1, 0);
  export const flipOutY = flipOut(0, 1);

  export const zoomIn = animation(
    [
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            opacity: 0,
            transform: 'scale3d(.3, .3, .3)',
            offset: 0,
          }),
          style({
            opacity: 1,
            transform: 'scale3d(1, 1, 1)',
            offset: 0.5,
          }),
        ])
      ),
    ],
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );
  
  export function zoomInY(a: string, b: string): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s cubic-bezier(0.550, 0.055, 0.675, 0.190)',
        keyframes([
          style({
            opacity: 0,
            transform: `scale3d(.1, .1, .1) translate3d(0, {{ a }}, 0)`,
            offset: 0,
          }),
          style({
            opacity: 1,
            transform: `scale3d(.475, .475, .475) translate3d(0, {{ b }}, 0)`,
            offset: 0.6,
          }),
        ])
      ),
      { params: { timing: DEFAULT_TIMING, delay: 0, a, b } }
    );
  }
  
  export function zoomInX(a: string, b: string): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s cubic-bezier(0.550, 0.055, 0.675, 0.190)',
        keyframes([
          style({
            opacity: 0,
            transform: `scale3d(.1, .1, .1) translate3d({{ a }}, 0, 0)`,
            offset: 0,
          }),
          style({
            opacity: 1,
            transform: `scale3d(.475, .475, .475) translate3d({{ b }}, 0, 0)`,
            offset: 0.6,
          }),
        ])
      ),
      { params: { timing: DEFAULT_TIMING, delay: 0, a, b } }
    );
  }
  
  export const zoomInDown = zoomInY('-1000px', '10px');
  export const zoomInUp = zoomInY('1000px', '-10px');
  export const zoomInLeft = zoomInX('-1000px', '10px');
  export const zoomInRight = zoomInX('1000px', '-10px');
  
  export const zoomOut = animation(
    [
      animate(
        '{{ timing }}s {{ delay }}s',
        keyframes([
          style({
            opacity: 1,
            offset: 0,
          }),
          style({
            opacity: 0,
            transform: 'scale3d(.3, .3, .3)',
            offset: 0.5,
          }),
          style({
            opacity: 0,
            offset: 1,
          }),
        ])
      ),
    ],
    {
      params: { timing: DEFAULT_TIMING, delay: 0 },
    }
  );
  
  export function zoomOutY(a: string, b: string): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s cubic-bezier(0.550, 0.055, 0.675, 0.190)',
        keyframes([
          style({
            opacity: 1,
            transform: `scale3d(.475, .475, .475) translate3d(0, {{ a }}, 0)`,
            offset: 0.4,
          }),
          style({
            opacity: 0,
            transform: `scale3d(.1, .1, .1) translate3d(0, {{ b }}, 0)`,
            offset: 1,
          }),
        ])
      ),
      { params: { timing: DEFAULT_TIMING, delay: 0, a, b } }
    );
  }
  
  export function zoomOutX(a: string, b: string): AnimationReferenceMetadata {
    return animation(
      animate(
        '{{ timing }}s {{ delay }}s cubic-bezier(0.550, 0.055, 0.675, 0.190)',
        keyframes([
          style({
            opacity: 1,
            transform: `scale3d(.475, .475, .475) translate3d({{ a }}, 0, 0)`,
            offset: 0.4,
          }),
          style({
            opacity: 0,
            transform: `scale3d(.1, .1, .1) translate3d({{ b }}, 0, 0)`,
            offset: 1,
          }),
        ])
      ),
      { params: { timing: DEFAULT_TIMING, delay: 0, a, b } }
    );
  }
  
  export const zoomOutDown = zoomOutY('-60px', '2000px');
  export const zoomOutUp = zoomOutY('60px', '-2000px');
  export const zoomOutLeft = zoomOutX('42px', '-2000px');
  export const zoomOutRight = zoomOutX('-42px', '2000px');