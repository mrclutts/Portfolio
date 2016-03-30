function fahrenheit() {
var temp1 = parseInt(document.getElementById('temp1').value);
	var celsius = (temp1 - 32)/(9/5);
	var c = celsius.toFixed(1);
	document.getElementById("printcel").innerHTML = temp1 + "&deg;F is equal to " + c + "&deg; C!";
}	
function celsius() {
	var temp2 = parseInt(document.getElementById('temp2').value);
	var fahrenheit = ((temp2 * (9/5)) + 32);
	var f = fahrenheit.toFixed(1);
	document.getElementById("printfahr").innerHTML = temp2 + "&deg; C is equal to " + f + "&deg; F!";
}		


