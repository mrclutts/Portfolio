function armstrong() {
for (var i = 100; i <1000; i++) {
	var str = i.toString()
	var a = parseInt(str.substring(0,1));
	var b = parseInt(str.substring(1,2));
	var c = parseInt(str.substring(2,3));
		if (Math.pow(a, 3) + Math.pow(b, 3) + Math.pow(c, 3) === i) {
		    document.getElementById("showArm").innerHTML += i + " ";
		}
	}
}
