

function routeDetails(id) {
    document.getElementById("route-value").value = id;
    document.getElementById("route-details-form").submit();
}

function booking(ticketClass, id) {
    document.getElementById('ticketClass').value = ticketClass;
    document.getElementById('route').value = id;
    document.getElementById('book-ticket').submit();
}


