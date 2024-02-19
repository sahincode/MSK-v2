var candidateBoxs = document.querySelectorAll('.candidate_voting_btn');
candidateBoxs.forEach(candidateBox => {
    var url = candidateBox.getAttribute('href');
    candidateBox.addEventListener('click', function (e) {
        e.preventDefault();
        fetch(`${url}`)

    })
})
//submit button script start 
document.addEventListener("DOMContentLoaded", function () {
    var checkboxes = document.querySelectorAll('.checkbox-input-candidate');

    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            checkboxes.forEach(function (otherCheckbox) {
                if (otherCheckbox !== checkbox) {
                    otherCheckbox.checked = false;
                }
            });
        });
    });
});