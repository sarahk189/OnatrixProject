
document.addEventListener('DOMContentLoaded', function () {
    const section = document.getElementById('why-choose-us');
    const container = section.querySelector('.container');

    function isInViewport(element) {
        const rect = element.getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );
    }

    function onScroll() {
        if (isInViewport(section)) {
            container.classList.add('in-view');
        } else {
            container.classList.remove('in-view');
        }
    }

    window.addEventListener('scroll', onScroll);
    onScroll(); 
});