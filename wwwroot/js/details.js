// JavaScript to handle scroll behavior for the related books section
document.addEventListener('DOMContentLoaded', function () {
    const scrollContainer = document.querySelector('.realted_books');
    const scrollLeftBtn = document.querySelector('.scroll-left');
    const scrollRightBtn = document.querySelector('.scroll-right');

    // Scroll the container to the left when the left button is clicked
    scrollLeftBtn.addEventListener('click', function () {
        scrollContainer.scrollBy({
            left: -300, // Scroll left by 300px (you can adjust this value)
            behavior: 'smooth'
        });
    });

    // Scroll the container to the right when the right button is clicked
    scrollRightBtn.addEventListener('click', function () {
        scrollContainer.scrollBy({
            left: 300, // Scroll right by 300px (you can adjust this value)
            behavior: 'smooth'
        });
    });
});