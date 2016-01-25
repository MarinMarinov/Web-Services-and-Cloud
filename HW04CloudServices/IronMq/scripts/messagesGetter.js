(function () {
    var Project_Id = '5648994c4aa03100090000a0';
    var Access_Token = 'ZFOE5vf6kPpILecVvfVc';
    var Get_Url = 'https://mq-aws-us-east-1.iron.io:443/1/projects/' +
        Project_Id +
        '/queues/global/messages';

    var messagesContainer = $('#msg-container')

    var options = {
        headers: {
            'Authorization': 'OAuth ' + Access_Token
        }
    };

    getMessage();
    setInterval(getMessage, 300);

    function getMessage() {
        jsonRequester.get(Get_Url, options)
            .then(function (res) {
                if (res.messages[0] !== undefined) {
                    var messageValue = res.messages[0].body;
                    var currentId = res.messages[0].id;
                    messagesContainer.append(messageValue + '\n');
                    
                    jsonRequester.delete(Get_Url + '/' + currentId, options);
                }
            })
    }
} ());