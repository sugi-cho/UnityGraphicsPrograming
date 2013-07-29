// easing functions from jQuery Easing plugin 1.3
// http://gsgd.co.uk/sandbox/jquery/easing/
using UnityEngine;

public static class Easing
{
	public static float easeInQuad (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * (t /= d) * t + b;
	}

	public static float easeOutQuad (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return -c * (t /= d) * (t - 2) + b;
	}

	public static float easeInOutQuad (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		if ((t /= d / 2f) < 1f)
			return c / 2f * t * t + b;
		return -c / 2f * ((--t) * (t - 2f) - 1f) + b;
	}

	public static float easeInCubic (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * (t /= d) * t * t + b;
	}

	public static float easeOutCubic (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * ((t = t / d - 1f) * t * t + 1f) + b;
	}

	public static float easeInOutCubic (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		if ((t /= d / 2f) < 1f)
			return c / 2f * t * t * t + b;
		return c / 2f * ((t -= 2f) * t * t + 2f) + b;
	}

	public static float easeInQuart (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * (t /= d) * t * t * t + b;
	}

	public static float easeOutQuart (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return -c * ((t = t / d - 1f) * t * t * t - 1f) + b;
	}

	public static float easeInOutQuart (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		if ((t /= d / 2f) < 1f)
			return c / 2f * t * t * t * t + b;
		return -c / 2f * ((t -= 2f) * t * t * t - 2f) + b;
	}

	public static float easeInQuint (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * (t /= d) * t * t * t * t + b;
	}

	public static float easeOutQuint (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * ((t = t / d - 1f) * t * t * t * t + 1f) + b;
	}

	public static float easeInOutQuint (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		if ((t /= d / 2f) < 1f)
			return c / 2f * t * t * t * t * t + b;
		return c / 2f * ((t -= 2f) * t * t * t * t + 2f) + b;
	}

	public static float easeInSine (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return -c * Mathf.Cos (t / d * (Mathf.PI / 2f)) + c + b;
	}

	public static float easeOutSine (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * Mathf.Sin (t / d * (Mathf.PI / 2f)) + b;
	}

	public static float easeInOutSine (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return -c / 2f * (Mathf.Cos (Mathf.PI * t / d) - 1f) + b;
	}

	public static float easeInExpo (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return (t == 0f) ? b : c * Mathf.Pow (2f, 10f * (t / d - 1f)) + b;
	}

	public static float easeOutExpo (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return (t == d) ? b + c : c * (- Mathf.Pow (2f, -10f * t / d) + 1f) + b;
	}

	public static float easeInOutExpo (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		if (t == 0f)
			return b;
		if (t == d)
			return b + c;
		if ((t /= d / 2f) < 1f)
			return c / 2f * Mathf.Pow (2f, 10f * (t - 1f)) + b;
		return c / 2f * (- Mathf.Pow (2f, -10f * --t) + 2f) + b;
	}

	public static float easeInCirc (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return -c * (Mathf.Sqrt (1f - (t /= d) * t) - 1f) + b;
	}

	public static float easeOutCirc (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		return c * Mathf.Sqrt (1f - (t = t / d - 1f) * t) + b;
	}

	public static float easeInOutCirc (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		if ((t /= d / 2f) < 1f)
			return -c / 2f * (Mathf.Sqrt (1f - t * t) - 1f) + b;
		return c / 2f * (Mathf.Sqrt (1f - (t -= 2f) * t) + 1f) + b;
	}

	public static float easeInElastic (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		float s = 1.70158f;
		float p = 0f;
		float a = c;
		if (t == 0)
			return b;
		if ((t /= d) == 1f)
			return b + c;
		if (p == 0)
			p = d * .3f;
		if (a < Mathf.Abs (c)) {
			a = c;
			s = p / 4f;
		} else
			s = p / (2f * Mathf.PI) * Mathf.Asin (c / a);
		return -(a * Mathf.Pow (2f, 10f * (t -= 1f)) * Mathf.Sin ((t * d - s) * (2f * Mathf.PI) / p)) + b;
	}

	public static float easeOutElastic (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		float s = 1.70158f;
		float p = 0f;
		float a = c;
		if (t == 0)
			return b;
		if ((t /= d) == 1f)
			return b + c;
		if (p == 0)
			p = d * .3f;
		if (a < Mathf.Abs (c)) {
			a = c;
			s = p / 4f;
		} else
			s = p / (2f * Mathf.PI) * Mathf.Asin (c / a);
		return a * Mathf.Pow (2f, -10f * t) * Mathf.Sin ((t * d - s) * (2f * Mathf.PI) / p) + c + b;
	}

	public static float easeInOutElastic (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		float s = 1.70158f;
		float p = 0f;
		float a = c;
		if (t == 0)
			return b;
		if ((t /= d / 2f) == 2f)
			return b + c;
		if (p == 0)
			p = d * (.3f * 1.5f);
		if (a < Mathf.Abs (c)) {
			a = c;
			s = p / 4f;
		} else
			s = p / (2f * Mathf.PI) * Mathf.Asin (c / a);
		if (t < 1f)
			return -.5f * (a * Mathf.Pow (2f, 10f * (t -= 1f)) * Mathf.Sin ((t * d - s) * (2f * Mathf.PI) / p)) + b;
		return a * Mathf.Pow (2f, -10f * (t -= 1f)) * Mathf.Sin ((t * d - s) * (2f * Mathf.PI) / p) * .5f + c + b;
	}

	public static float easeInBack (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		float s = 1.70158f;
		return c * (t /= d) * t * ((s + 1f) * t - s) + b;
	}

	public static float easeOutBack (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		float s = 1.70158f;
		return c * ((t = t / d - 1f) * t * ((s + 1f) * t + s) + 1f) + b;
	}

	public static float easeInOutBack (float x)
	{
		float t = x;
		float b = 0f;
		float c = 1f;
		float d = 1f;
		float s = 1.70158f;
		if ((t /= d / 2f) < 1f)
			return c / 2f * (t * t * (((s *= (1.525f)) + 1f) * t - s)) + b;
		return c / 2f * ((t -= 2f) * t * (((s *= (1.525f)) + 1f) * t + s) + 2f) + b;
	}

	public static float easeOutBounce (float x, float t, float b, float c, float d)
	{
		if ((t /= d) < (1f / 2.75f)) {
			return c * (7.5625f * t * t) + b;
		} else if (t < (2f / 2.75f)) {
			return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
		} else if (t < (2.5f / 2.75f)) {
			return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
		} else {
			return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
		}
	}

	public static float easeOutBounce (float x)
	{
		return easeOutBounce (x, x, 0f, 1f, 1f);
	}

	public static float easeInBounce (float x, float t, float b, float c, float d)
	{
		return c - easeOutBounce (x, d - t, 0f, c, d) + b;
	}

	public static float easeInBounce (float x)
	{
		return easeInBounce (x, x, 0f, 1f, 1f);
	}

	public static float easeInOutBounce (float x, float t, float b, float c, float d)
	{
		if (t < d / 2f)
			return easeInBounce (x, t * 2f, 0f, c, d) * .5f + b;
		return easeOutBounce (x, t * 2f - d, 0f, c, d) * .5f + c * .5f + b;
	}

	public static float easeInOutBounce (float x)
	{
		return easeInOutBounce (x, x, 0f, 1f, 1f);
	}

}
