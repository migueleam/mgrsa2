
var $ = window.jQuery;
var $msg = $('#messages');
var fs = require('fs');

var commonnwG = {
      
    //codigo facilito...

    ajax: function (object) {
        return new Promise(function (resolve, reject){
            $.ajax(object).done(resolve).fail(reject)
        })
    },

    /////////// JEDUAN CORNEJO platzi

    waitTwoSeconts: function () {
        return new Promise(function (done, reject) {
            setTimeout(function () {
                done()
            }, 2000)
        });
    },

    // JOSE FRESO PLATZI


    readFile: function (name) {
        return new Promise(function (resolve, reject) {
            fs.readFile(name, function (err, content) {
                if (err) {
                    return reject(err)
                }
                resolve(content)
            });
        });
    },

    writeFile: function (name, content) {
        return new Promise(function (resolve, reject) {
            fs.writeFile(name, content, function (err) {
                resolve()
            })
        })
    }

}

//app


//APP
var applicar = {
    //codigo facilito
    ajaxEvent: function () {

        let username = document.getElementById('username').value;
        let password = document.getElementById('password').value;
        let method = document.getElementById('user-form').method;
        let action = document.getElementById('user-form').action;

        let payload = { username: username, password: password };
        let object = {
            url: action,
            type: method,
            contentType: 'application/json',
            data: payload
        };

        commonnwG.ajax(object).then(function resolve(data) {
            console.log(data);
        }, function reject(reason) {
            console.log('Algo salio mal');
            console.log(reason);
        });

    },
    //CORNEJO 

    cornejo: function () {

        $msg.text('quiero un espresso')
        commonnwG.waitTwoSeconts()
            .then(function (cafe) {
                $msg.text('aqui tiese su ' + cafe)
                return waitTwoSeconts()
            }).then(function (imagen) {
                //if
                throw 'esta muy caliente'
                $msg.text('tomar un cafe')
            })
            .catch(function (err) {
                $msg.text(err).css('color', 'red')
            });            
    },    

    //FRESCO

    frescoA: function () {

        readFile('./lorem.text')
            .then(content => writeFile('./cantidad.text', content.length))
            .catch(err => console.log('Hubo un errror: ' + err.message))

    },

    frescoB: function () {
        Promise.all([
            readFile('/lorem.txt'),
            readFile('./cantidad.txt'),
            readFile('./index.js')
        ]).then(responses => console.log(responses.length));

    }

}