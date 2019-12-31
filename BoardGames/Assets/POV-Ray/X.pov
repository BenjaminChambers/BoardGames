camera {
	orthographic
	up y*2.5
	right x*2.5
	
	location -z*10
	look_at 0
}

light_source {(x+y-z)*10 color rgb 1}
light_source {
	(z-x-y)*10 color rgb 1/2
	area_light x*5,y*5,5,5
	circular
}

#local lineWidth = 0.15;

union {
	cylinder {-x-y, x+y, lineWidth}
	cylinder {y-x, x-y, lineWidth}
	sphere {-x-y, lineWidth}
	sphere {x+y, lineWidth}
	sphere {y-x, lineWidth}
	sphere {x-y, lineWidth}
	
	texture {
		pigment {color rgb x}
		finish {specular 1 reflection 0.5}
	}
}
