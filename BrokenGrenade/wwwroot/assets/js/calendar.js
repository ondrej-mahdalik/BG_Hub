let calendar;

document.addEventListener('DOMContentLoaded', function () {
    setTimeout(InitCalendar, 1000);
});

function InitCalendar() {
    var calendarEl = document.getElementById('calendar');
    calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'cs',
        initialView: 'dayGridMonth',
        events: '/api/missions'
    });
    calendar.render();
    
    addEventListener('resize', function () {
        calendar.changeView("dayGridMonth");
    }, true);
}