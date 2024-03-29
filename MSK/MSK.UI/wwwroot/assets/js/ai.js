﻿
//ai integration
const token = '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';
const chatInput = document.getElementById('chatInput');
const chatBtn = document.getElementById('chatButton');
const chanAns = document.getElementById('chatAnswer');
const userQues = document.getElementById('userQuestion');
const aiLogo = document.getElementById('ai-logo');
const answerCon = document.getElementById('answerContainer');
const quesCon = document.getElementById('userAnsContainer');
const quesAnsContainer = document.getElementById('chatContainer');
const userSec = document.getElementById('userSection');
chatBtn?.addEventListener('click', function (e) {
    aiLogo.style.display = 'none';
    answerCon.style.display = 'inline-block';
    quesCon.style.display = 'block';
    userQues.innerText = chatInput.value;
    quesAnsContainer.classList.add('scroll-answer')
    userSec.classList.remove('d-none');

    e.preventDefault();
    fetch('https://api.openai.com/v1/chat/completions', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        body: JSON.stringify({
            "model": "gpt-3.5-turbo",
            "messages": [{ "role": "user", "content": chatInput.value }]
        })
    }).then(response => {
        return response.json();

    }).then(data => {
        simulateChat(data.choices[0].message.content);
         async function saveChat() {
            const formData = new FormData();
             formData.append("Question", userQues.innerText);
             formData.append("Answer", await data.choices[0].message.content);
            formData.append("ChatterId", null);

            fetch('/aisupport/SaveUserSection', {
                method: 'POST',
                body: formData,
            });

        }
        saveChat();
    })
  
    chatInput.value = "";

})
//ai support index page user profile script
document.addEventListener('DOMContentLoaded', function () {
    var firstName = document.getElementById('UserName')?.textContent;

  

    var profileImage1 = document.getElementById('userProImage1');
    var profileImage2 = document.getElementById('userProImage2');

    profileImage1.innerText = firstName.charAt(0);
    profileImage2.innerText = firstName.charAt(0);

});
document.addEventListener('DOMContentLoaded', function () {
    var textarea = document.getElementById('chatInput');


    textarea?.addEventListener('keydown', function (event) {
        if (event.key === 'Enter' && !event.shiftKey) {
            event.preventDefault();
            aiLogo.style.display = 'none';
            answerCon.style.display = 'inline-block';
            quesCon.style.display = 'block';
            userQues.innerText = chatInput.value;
            quesAnsContainer.classList.add('scroll-answer');
            userSec.classList.remove('d-none');


            fetch('https://api.openai.com/v1/chat/completions', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token,
                },
                body: JSON.stringify({
                    "model": "gpt-3.5-turbo",
                    "messages": [{ "role": "user", "content": chatInput.value }]
                })
            }).then(response => {
                return response.json();

            }).then(data => {
                simulateChat(data.choices[0].message.content);
               async  function saveChat() {
                    const formData = new FormData();
                   formData.append("Question", userQues.innerText);
                    formData.append("Answer",  await data.choices[0].message.content);
                    formData.append("ChatterId", null);

                    fetch('/aisupport/SaveUserSection', {
                        method: 'POST',
                        body: formData,
                    });

                }
                saveChat()
            })
            chatInput.value = "";
        }
    });
});
function simulateChat(response) {
    const lines = response.split('<br>');
    let delay = 1000000; // Set your desired delay between lines (in milliseconds)

    lines.forEach((line, index) => {
        setTimeout(() => {
            chanAns.innerText = `${line}`;
            // Scroll to the bottom after each line (optional)
            chanAns.scrollTop = chanAns.scrollHeight;
        }, delay * index);
    });
}

var oldQuestions = document.querySelectorAll(".old-question");
oldQuestions.forEach(q => q.addEventListener('click', function (e) {
    e.preventDefault();
    aiLogo.style.display = 'none';
    answerCon.style.display = 'inline-block';
    quesCon.style.display = 'block';
    userQues.innerText = q.innerText;
    userSec.classList.remove('d-none');
    quesAnsContainer.classList.add('scroll-answer')
    fetch('https://api.openai.com/v1/chat/completions', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        body: JSON.stringify({
            "model": "gpt-3.5-turbo",
            "messages": [{ "role": "user", "content": q.innerText }]
        })
    }).then(response => {
        return response.json();

    }).then(data => {
        simulateChat(data.choices[0].message.content);
        async function saveChat() {
            const formData = new FormData();
            formData.append("Question", userQues.innerText);
            formData.append("Answer", await data.choices[0].message.content);
            formData.append("ChatterId", null);

            fetch('/aisupport/SaveUserSection', {
                method: 'POST',
                body: formData,
            });

        }
        saveChat()
    })
    chatInput.value = "";
}))

///sidebar  script start 
//hamburger click action 
const menuBtn = document.querySelector('.hamburger');
const menuDiv = document.querySelectorAll('.nav-mobile');
const body = document.querySelector('.body-layout');
const divOver = document.querySelector('.div-over');



menuBtn?.addEventListener('click', function () {

    menuBtn.classList.toggle('is-active');
    menuDiv.forEach(menu => menu.classList.toggle('active'));

    divOver.classList.toggle('dark-back');

    body.classList.toggle('o-hidden');

});
//var deleteQuestions = document.querySelectorAll(".delete__question");

//deleteQuestions.forEach(dq => {
    
//    async function deleteChat(e) {
//        var parent = dq.parentElement;
//        var grandParent = parent.parentElement;
//        e.preventDefault();
//        const formData = new FormData();
//        formData.append("Question", userQues.innerText);
//        formData.append("Question", grandParent.innerText);
//        formData.append("Answer", await data.choices[0].message.content);
//        formData.append("ChatterId", null);

//        fetch('/aisupport/DeleteUserSection', {
//            method: 'POST',
//            body: formData,
//        });
//    }
//    dq.addEventListener('click', deleteChat)
//})

