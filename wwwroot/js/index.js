const btnPrev = document.querySelector('#scrollerControlButtonPrev');
const btnNext = document.querySelector('#scrollerControlButtonNext');
const sliderContent = document.querySelector('#scrollerContent');
const sliderItems = Array.from(sliderContent.children);

var index = 0;
var countMassSlide = sliderItems.length;
var firstSlideWidth = 100;
var secondSlideWidth = 0;
var MoveSliderBool = true;
function MoveSlideLeft (firstSlide, secondSlide){
    if(index != countMassSlide - 1){
        firstSlide.classList.add('scrollerContentImgLeft');
        firstSlide.classList.remove('scrollerContentImgMiddle');
        secondSlide.classList.add('scrollerContentImgMiddle');
        secondSlide.classList.remove('scrollerContentImgRight');
        index++;
    }
}
function MoveSlideRight (firstSlide, secondSlide){
    if(index != 0){
        firstSlide.classList.add('scrollerContentImgRight');
        firstSlide.classList.remove('scrollerContentImgMiddle');
        secondSlide.classList.add('scrollerContentImgMiddle');
        secondSlide.classList.remove('scrollerContentImgLeft');
        index--;
    }
}
btnNext.onclick = function() {
    MoveSlideLeft(sliderItems[index],sliderItems[index + 1]); 
    clearSliderForTime();
}
btnPrev.onclick = function() {
    MoveSlideRight(sliderItems[index],sliderItems[index - 1]); 
    clearSliderForTime();
}
var intervalSliderForTime = 4000;
MoveSliderForTime();
function MoveSliderForTime(){
    if(MoveSliderBool){
        if(index == 0){
            var MoveSliderForTimeIntervalLeft = setInterval(() => {
                if(MoveSliderBool){
                    MoveSlideLeft(sliderItems[index],sliderItems[index + 1]); 
                } else{
                    clearInterval(MoveSliderForTimeIntervalLeft); 
                }
            }, intervalSliderForTime);
            setTimeout(() => { 
                clearInterval(MoveSliderForTimeIntervalLeft); 
                MoveSliderForTime()
            }, 
            intervalSliderForTime*countMassSlide + 500);
        }
        if(index >= 1){
            var MoveSliderForTimeIntervalRight = setInterval(() => {
                if(MoveSliderBool){
                    MoveSlideRight(sliderItems[index],sliderItems[index - 1]);
                } else{
                    clearInterval(MoveSliderForTimeIntervalRight); 
                }
            }, intervalSliderForTime);
            setTimeout(() => { 
                clearInterval(MoveSliderForTimeIntervalRight); 
                MoveSliderForTime()
            }, 
            intervalSliderForTime*index + 500);
        }
    }
}
function clearSliderForTime(){
    MoveSliderBool = false;
    setTimeout(() => { 
        if(!MoveSliderBool){
            MoveSliderBool = true;
            MoveSliderForTime()
        }
    }, 15000);
}