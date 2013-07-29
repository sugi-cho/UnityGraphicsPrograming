#ifndef BLENDING_INCLUDED
#define BLENDING_INCLUDED

float3 overlay(float3 c1, float3 c2){
	fixed3 check = step(0.5, c1.rgb);
	fixed3 result = 0;
	result =  check * (half3(1,1,1) - ( (half3(1,1,1) - 2*(c1-0.5)) *  (1-c2))); 
	result += (1-check) * (2*c1) * c2;
	return result;
}

#endif // BLENDING_INCLUDED
