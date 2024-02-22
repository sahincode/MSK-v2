
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
chatBtn?.addEventListener('click', function (e) {
    aiLogo.style.display = 'none';
    answerCon.style.display = 'inline-block';
    quesCon.style.display = 'block';
    userQues.innerText = chatInput.value;
    quesAnsContainer.classList.add('scroll-answer')

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
        chanAns.innerText = data.choices[0].message.content;
    })
    chatInput.value = "";

})
//ai support index page user profile script
document.addEventListener('DOMContentLoaded', function () {
    var firstName = document.getElementById('UserName')?.textContent;

  

    var profileImage = document.getElementById('userProImage');
    profileImage.innerText = firstName.charAt(0);
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
            quesAnsContainer.classList.add('scroll-answer')

            event.preventDefault();
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
                chanAns.innerText = data.choices[0].message.content;
            })
            textarea.value = "";
        }
    });
});

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

})
