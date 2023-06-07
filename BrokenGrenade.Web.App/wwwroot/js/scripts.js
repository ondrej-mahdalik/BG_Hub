function InitTooltips() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
}

function EmulateClick(elementId) {
    const element = document.getElementById(elementId);
    element.click();
}

function CollapseSidebar(sidebarId, buttonId) {
    const element = document.getElementById(sidebarId);

    if (element.classList.contains("show")) {
        EmulateClick(buttonId);
    }
}