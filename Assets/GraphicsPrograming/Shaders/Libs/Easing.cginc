#ifndef EASING_INCLUDED
#define EASING_INCLUDED

//easing func from jQuery Easing Plugin
// http://gsgd.co.uk/sandbox/jquery/easing/
float easeInQuad(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c*(t/=d)*t + b;
}
float easeOutQuad(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return -c *(t/=d)*(t-2) + b;
}
float easeInOutQuad(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	if ((t/=d/2) < 1) return c/2*t*t + b;
	return -c/2 * ((--t)*(t-2) - 1) + b;
}
float easeInCubic(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c*(t/=d)*t*t + b;
}
float easeOutCubic(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c*((t=t/d-1)*t*t + 1) + b;
}
float easeInOutCubic(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	if ((t/=d/2) < 1) return c/2*t*t*t + b;
	return c/2*((t-=2)*t*t + 2) + b;
}
float easeInQuart(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c*(t/=d)*t*t*t + b;
}
float easeOutQuart(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return -c * ((t=t/d-1)*t*t*t - 1) + b;
}
float easeInOutQuart(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	if ((t/=d/2) < 1) return c/2*t*t*t*t + b;
	return -c/2 * ((t-=2)*t*t*t - 2) + b;
}
float easeInQuint(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c*(t/=d)*t*t*t*t + b;
}
float easeOutQuint(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c*((t=t/d-1)*t*t*t*t + 1) + b;
}
float easeInOutQuint(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	if ((t/=d/2) < 1) return c/2*t*t*t*t*t + b;
	return c/2*((t-=2)*t*t*t*t + 2) + b;
}
float easeInSine(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return -c * cos(t/d * (3.14159265359/2)) + c + b;
}
float easeOutSine(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c * sin(t/d * (3.14159265359/2)) + b;
}
float easeInOutSine(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return -c/2 * (cos(3.14159265359*t/d) - 1) + b;
}
float easeInExpo(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return (t==0) ? b : c * pow(2, 10 * (t/d - 1)) + b;
}
float easeOutExpo(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return (t==d) ? b+c : c * (-pow(2, -10 * t/d) + 1) + b;
}
float easeInOutExpo(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	if (t==0) return b;
	if (t==d) return b+c;
	if ((t/=d/2) < 1) return c/2 * pow(2, 10 * (t - 1)) + b;
	return c/2 * (-pow(2, -10 * --t) + 2) + b;
}
float easeInCirc(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return -c * (sqrt(1 - (t/=d)*t) - 1) + b;
}
float easeOutCirc(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	return c * sqrt(1 - (t=t/d-1)*t) + b;
}
float easeInOutCirc(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	if ((t/=d/2) < 1) return -c/2 * (sqrt(1 - t*t) - 1) + b;
	return c/2 * (sqrt(1 - (t-=2)*t) + 1) + b;
}
float easeInElastic(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	float s=1.70158;float p=0;float a=c;
	if (t==0) return b;  if ((t/=d)==1) return b+c;  if (p == 0) p=d*.3;
	if (a < abs(c)) { a=c; s=p/4; }
	else s = p/(2*3.14159265359) * asin (c/a);
	return -(a*pow(2,10*(t-=1)) * sin( (t*d-s)*(2*3.14159265359)/p )) + b;
}
float easeOutElastic(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	float s=1.70158;float p=0;float a=c;
	if (t==0) return b;  if ((t/=d)==1) return b+c;  if (p ==0) p=d*.3;
	if (a < abs(c)) { a=c; s=p/4; }
	else s = p/(2*3.14159265359) * asin (c/a);
	return a*pow(2,-10*t) * sin( (t*d-s)*(2*3.14159265359)/p ) + c + b;
}
float easeInOutElastic(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	float s=1.70158;float p=0;float a=c;
	if (t==0) return b;  if ((t/=d/2)==2) return b+c;  if (p ==0) p=d*(.3*1.5);
	if (a < abs(c)) { a=c; s=p/4; }
	else s = p/(2*3.14159265359) * asin (c/a);
	if (t < 1) return -.5*(a*pow(2,10*(t-=1)) * sin( (t*d-s)*(2*3.14159265359)/p )) + b;
	return a*pow(2,-10*(t-=1)) * sin( (t*d-s)*(2*3.14159265359)/p )*.5 + c + b;
}
float easeInBack(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	float s = 1.70158;
	return c*(t/=d)*t*((s+1)*t - s) + b;
}
float easeOutBack(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	float s = 1.70158;
	return c*((t=t/d-1)*t*((s+1)*t + s) + 1) + b;
}
float easeInOutBack(float x) {
	float t = x; float b = 0; float c = 1; float d = 1;
	float s = 1.70158;
	if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525))+1)*t - s)) + b;
	return c/2*((t-=2)*t*(((s*=(1.525))+1)*t + s) + 2) + b;
}
float easeOutBounce(float x, float t, float b, float c, float d) {
	if ((t/=d) < (1/2.75)) {
		return c*(7.5625*t*t) + b;
	} else if (t < (2/2.75)) {
		return c*(7.5625*(t-=(1.5/2.75))*t + .75) + b;
	} else if (t < (2.5/2.75)) {
		return c*(7.5625*(t-=(2.25/2.75))*t + .9375) + b;
	} else {
		return c*(7.5625*(t-=(2.625/2.75))*t + .984375) + b;
	}
}
float easeOutBounce(float x){
	return easeOutBounce(x, x, 0, 1, 1);
}
float easeInBounce(float x, float t, float b, float c, float d) {
	return c - easeOutBounce (x, d-t, 0, c, d) + b;
}
float easeInBounce(float x){
	return easeInBounce(x, x, 0, 1, 1);
}
float easeInOutBounce(float x, float t, float b, float c, float d) {
	if (t < d/2) return easeInBounce (x, t*2, 0, c, d) * .5 + b;
	return easeOutBounce (x, t*2-d, 0, c, d) * .5 + c*.5 + b;
}
float easeInOutBounce(float x){
	return easeInOutBounce(x, x, 0, 1, 1);
}

#endif // EASING_INCLUDED
