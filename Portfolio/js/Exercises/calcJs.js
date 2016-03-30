
	function calculate() {
		var userInput = document.getElementById('display').value;
		var result = eval(userInput);
		document.getElementById("display").value = result;
	}
