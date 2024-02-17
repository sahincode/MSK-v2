(function () {
    const second = 1000,
        minute = second * 60,
        hour = minute * 60,
        day = hour * 24;

    //I'm adding this section so I don't have to keep updating this pen every year :-)
    //remove this if you don't need it
    let today = new Date(),
        dd = String(today.getDate()).padStart(2, "0"),
        mm = String(today.getMonth() + 1).padStart(2, "0"),
        yyyy = today.getFullYear(),
        nextYear = yyyy + 1,
        dayMonth = "02/07/",
        birthday = dayMonth + yyyy;

    today = mm + "/" + dd + "/" + yyyy;
    if (today > birthday) {
        birthday = dayMonth + nextYear;
    }

    const countDown = new Date(birthday).getTime(),
        x = setInterval(function () {

            const now = new Date().getTime(),
                distance = countDown - now;

            document.getElementById("days").innerText = Math.floor(distance / (day)),
                document.getElementById("hours").innerText = Math.floor((distance % (day)) / (hour) + 8),
                document.getElementById("minutes").innerText = Math.floor((distance % (hour)) / (minute)),
                document.getElementById("seconds").innerText = Math.floor((distance % (minute)) / second);

            //do something later when date is reached
            if (distance < 0) {
                document.getElementById("countdown").style.display = "none";
                clearInterval(x);
            }
            //seconds
        }, 0)
}());
const navButton = document.querySelector('.navbar-toggler');
const navDiv = document.querySelector('.navbar-collapse');

navButton.addEventListener("click", function () {

    if (navButton.classList.contains('collapsed') && !navButton.classList.contains('show')) {
        navButton.classList.remove('collapsed')
        navDiv.classList.add('show');
    }

    else {
        navButton.classList.add('collapsed');
        navDiv.classList.remove('show');

    }

})
const articles = document.querySelectorAll('.article-active');
const articleContent = document.querySelector('.article-content');
const articleImg = document.querySelector('.image-article');


articles.forEach(article => article.addEventListener('mouseover', function () {

    var previouslyActive = document.querySelector('.active-press');
    if (previouslyActive) {
        previouslyActive.classList.remove('active-press');
    }
    articleContent.innerHTML = '';
    if (articleContent.innerHTML === '') {
        articleContent.innerHTML = article.innerHTML;
    }
    articleImg.src = article.getAttribute('data-image');
    article.classList.add('active-press');
}))
// speech search functionality in layout 
const searchForm = document.getElementById('form-search');
const searchContainer = document.getElementById('searchContainer');

const formInput = searchForm.querySelector('input');


const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
if (SpeechRecognition) {
    searchContainer.insertAdjacentHTML("beforeend", '<button><i class="fa-solid fa-microphone mb - 2"></i></button>');
    const srcBtn = searchContainer.querySelector('button');
    const micIcon = searchContainer.querySelector('i');
    const recognition = new SpeechRecognition();
    srcBtn.addEventListener('click', micBtnClick)
    function micBtnClick(e) {
        e.preventDefault();
        if (micIcon.classList.contains("fa-microphone")) { //start speech recoginition 

            recognition.start();

        } else { //end speech recognition

            recognition.stop();
        }
    }
    recognition.addEventListener("start", startSpeechRecognition);
    function startSpeechRecognition() {
        micIcon.classList.remove("fa-microphone");
        micIcon.classList.add("fa-stop");
        console.log("start");

    }
    recognition.addEventListener("end", stopSpeechRecognition);
    function stopSpeechRecognition() {
        micIcon.classList.remove("fa-stop");
        micIcon.classList.add("fa-microphone");
        console.log("end");
    }
    recognition.addEventListener("result", resultOfSpeechRecognition);
    function resultOfSpeechRecognition(event) {
        const currentReslutIndex = event.resultIndex;
        const transcript = event.results[currentReslutIndex][0].transcript;
        const cleanResult = transcript.replace(/[.,\/#!$%\^&\*;:{}=\-_`~()]/g, '').trim()
        if (transcript.toLowerCase().trim() === "stop recording.") {

            recognition.stop();

        }

        else {
            if (cleanResult.toLowerCase().trim() === "go google") {

                window.open("https://www.google.com", "_blank");

            }
            else if (cleanResult.toLowerCase().trim() === "open ai support") {
                window.location.href = "https://localhost:7090/aisupport/index"
            }
            else {
                formInput.value = cleanResult;
                setTimeout(() => {
                    searchForm.submit();
                }, 1000)
            }

        }


    }
}
//Ivoting pages script

//document.querySelector('#contact-form').addEventListener('submit', (e) => {
//    e.preventDefault();
//    e.target.elements.name.value = '';
//    e.target.elements.email.value = '';
//    e.target.elements.message.value = '';
//});

// image  verification script
const video = document.getElementById('camera');
const captureBtn = document.getElementById('captureBtn');
const userLoginImage = document.getElementById('userLoginImage');
const finCodeinput = document.getElementById('finCodeInput')
let cameraOn = false;
let capturedImage;
captureBtn?.addEventListener('click', (e) => {
    e.preventDefault();
    if (!cameraOn) {
        userLoginImage.style.display = 'none';
        video.style.display = 'inline-block';

        navigator.mediaDevices.getUserMedia({ video: true }).then((stream) => {
            video.srcObject = stream;
            cameraOn = true;
            video.play();
        }).catch((error) => {
            console.log('error accesing camera:', error);
        })
    }
    else {

        const context = userLoginImage.getContext('2d');
         capturedImage = userLoginImage.toDataURL('image/png');
        context.drawImage(video, 0, 0, userLoginImage.width, userLoginImage.height);

        video.style.display = 'none';

        userLoginImage.style.display = 'block';
        const tracks = video.srcObject.getTracks();
        tracks.forEach(track => track.stop());
        video.srcObject = null;
        cameraOn = false;

    }
    
})
const ivoteringSubBtn = document.getElementById('ivoteringSubmit');

function dataURLtoBlob(dataURL) {
    const byteString = atob(dataURL.split(',')[1]);
    const mimeString = dataURL.split(',')[0].split(':')[1].split(';')[0];

    const arrayBuffer = new ArrayBuffer(byteString.length);
    const uint8Array = new Uint8Array(arrayBuffer);

    for (let i = 0; i < byteString.length; i++) {
        uint8Array[i] = byteString.charCodeAt(i);
    }

    return new Blob([arrayBuffer], { type: mimeString });
}

function blobToFormFile(blob, fileName) {
    const formFile = new File([blob], fileName, { type: blob.type });
    return formFile;
}
ivoteringSubBtn?.addEventListener('click', function (e) {
    e.preventDefault();
    const blob = dataURLtoBlob(capturedImage);
    const formFile = blobToFormFile(blob, 'captured-image.png');
    const formData = new FormData();
    formData.append('Image', formFile);
    formData.append('FinCode', finCodeinput.value);
    fetch('/ivoting/login', {
        method: 'POST',
        body: formData,
    });
});
//ivoting login page video helper script
    var videoHelper = document.getElementById('helpVideo');
var playIcon = document.getElementById('playIcon');
let isPlaying = false;
playIcon.addEventListener('click', function () {
    if (!isPlaying) {
        playIcon.style.opacity = '0';
            videoHelper.controls = true;
        videoHelper.play();
        isPlaying = true;
        }
        else {
            playIcon.style.opacity = '1';
            videoHelper.controls = false;
        videoHelper.pause();
        isPlaying = false;
        }
    })

