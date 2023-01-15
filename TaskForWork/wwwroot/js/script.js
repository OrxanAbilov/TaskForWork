let add = document.querySelector('.create');
let update = document.querySelector('.update');
let modal = document.querySelector('.modal');
let cancel = document.querySelector('.cancel');
let header = document.querySelector('.header');
let main = document.querySelector('.main');


add.addEventListener('click', () => {
    modal.style.transform = 'scale(1)';
    header.classList.add('hidden');
    main.classList.add('hidden');
    
    
})

update.addEventListener('click', () => {
    modal.style.transform = 'scale(1)';
    header.classList.add('hidden');
    main.classList.add('hidden');
})


cancel.addEventListener('click', () => {
    modal.style.transform = 'scale(0)';
    header.classList.remove('hidden');
    main.classList.remove('hidden');
})


var elements = document.getElementsByClassName("body-line");

for (var i = 0; i < elements.length; i++) {
    elements[i].onclick = function () {
        var el = elements[0];
        while (el) {
            if (el.tagName === "DIV") {
                el.classList.remove("body-line-onclick");

            }
            el = el.nextSibling;
        }
        this.classList.add("body-line-onclick");
    };
}





