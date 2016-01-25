/* global PUBNUB */
(function () {
    var sendButton = $('#btn-send');
    var inputField = $('#input');
    var messageContainer = $('#msg-container');

    var pubnub = PUBNUB({
        subscribe_key: 'demo', // always required
        publish_key: 'demo'    // only required if publishing
    });

    sendButton.on('click', function () {
        var messageValue = inputField.val();
        inputField.val('');
        pubnub.publish({
            channel: 'my_channel',
            message: myip + ': ' + messageValue,
            callback: function (m) { }
        });
    });

    pubnub.subscribe({
        channel: 'my_channel',
        message: function (m) {
            appendMessage(m);
        }
    });

    function appendMessage(message) {
        messageContainer.append(message + '\n');
    }
    
    inputField.keyup(function (event) {
		if (event.keyCode == 13) {
			sendButton.click();
		}
	});

} ());