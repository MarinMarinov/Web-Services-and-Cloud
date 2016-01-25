(function () {
	var Project_Id = '5648994c4aa03100090000a0';
	var Access_Token = 'ZFOE5vf6kPpILecVvfVc';
	var Post_Url = 'https://mq-aws-us-east-1.iron.io:443/1/projects/' +
		Project_Id +
		'/queues/global/messages';
		

	var sendButton = $('#btn-send');
	var inputMessage = $('#input');
    var currentChannel = 'global';

	sendButton.on('click', function () {
		var messageValue = inputMessage.val();
		if (messageValue) {
			var options = {
				headers: {
					'Authorization': 'OAuth ' + Access_Token
				},
				data: {
					'messages': [
						{
							'body':myip + ': ' + messageValue
						}
					]
				}
			};
			
			jsonRequester.post(Post_Url, options)
			inputMessage.val('');
		}
	});
	
	inputMessage.keyup(function (event) {
		if (event.keyCode == 13) {
			sendButton.click();
		}
	});
} ());