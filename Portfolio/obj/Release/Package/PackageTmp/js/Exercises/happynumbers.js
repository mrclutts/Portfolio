function happyNo() {
for (var i = 1; i <20; i++) {
	var str = i.toString();
	for (var j = 1; j<10; j++) {
		
		var a = parseInt(str.substring(0,1));
		var c = Math.pow(a, 2);
		if (str.length < 2) {
			var d = 0;
		}
		else {
			var b = parseInt(str.substring(1,2));
			var d = Math.pow(b, 2);	
		}
		var result = c + d;
			if (result === 1) {
			    document.getElementById("showhappy").innerHTML += i + " ";
				break;
			}
			else {
				 str = result.toString();
			}
		}
	}
}