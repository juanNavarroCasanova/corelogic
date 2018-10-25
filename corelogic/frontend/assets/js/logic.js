/*!
* These are the main logic scripts
*
*/
"use strict";

// this is the global filter object
let filter = {
  practitionerId: "",
  startDate: null,
  endDate: null
};

/**
 * This will apply a new filter
 * @param {*} pid practitionerId
 * @param {*} startDate
 * @param {*} endDate
 */
function applyFilter (pid, startDate, endDate) {
  if(pid) {
    filter.practitionerId = pid;
  }
  if(startDate) {
    filter.startDate = startDate;
  }
  if(endDate) {
    filter.endDate = endDate;
  }
  getReport();
};

function applyDateRange() {
  // this will respect whatever partitioner is selected but change the dates
  filter.startDate = jQuery("#startDate").val() || null;
  filter.endDate = jQuery("#endDate").val() || null;
  getReport();
}

/**
 * This is just a helper function to create the query string I need for the API to filter
 * @param {practitionerId} practitionerId
 */
function getQueryString(practitionerId, yearMonth) {
  let pid = (practitionerId || filter.practitionerId) || "";
  let queryString = [];
  if (pid) {
    queryString.push("pid=" + pid);
  }
  if(yearMonth) {
    let year = yearMonth.substring(0,4);
    let month = yearMonth.substring(4);
    let thisStartDate = moment(month + "-" + year, "MM-YYYY").startOf('month').format("DD/MM/YYYY");
    let thisEndDate = moment(month + "-" + year, "MM-YYYY").endOf('month').format("DD/MM/YYYY");
    queryString.push("startDate=" + thisStartDate);
    queryString.push("endDate=" + thisEndDate);
  } else {
    if (filter.startDate) {
      queryString.push("startDate=" + filter.startDate);
    }
    if (filter.endDate) {
      queryString.push("endDate=" + filter.endDate);
    }
  }
  if(queryString.length > 0) {
    queryString[0] = "?" + queryString[0];
    if (queryString.length == 2) {
      queryString[1] = "&" + queryString[1];
    }
    if (queryString.length == 3) {
      queryString[1] = "&" + queryString[1];
      queryString[2] = "&" + queryString[2];
    }
  }
  return queryString.join("");
}

/**
 * All of them, to list them
 */
function getPractitioners() {
  let listItems = [];
  jQuery.get("http://localhost:59489/api/practitioners").done(data => {
    data.forEach(element => {
      listItems.push(`<li class="list-group-item"><a href="#" onClick='applyFilter(${element.id}); return false;'>${element.name}</a></li>`)
    });
    jQuery("#practitioners-list").append(listItems.join(""));
  }).fail(err => {
    console.error("error getting the list of practitioners");
  });
};

function getReport() {
  let queryString = getQueryString();

  // empty the lists
  jQuery('#report-list').html("");
  jQuery('#appointment-list').html("");
  jQuery('#appointment-details').html("");

  let listItems = [];
  jQuery.get("http://localhost:59489/api/report" + queryString).done(data => {
    data.forEach(element => {
      listItems.push(`<div class="card">
      <div class="card-body">
        <div class="card-title like-a-toolbar">
          <h5>${element.name}</h5>
          <a href='#' onClick='getAppointments(${element.practitioner_id}, ${element.month}); return false;'>Appointments</a>
        </div>
        <h5 class="card-title">${moment(element.month,"YYYYMM").format("M of Y")}</h5>
        <div class="card-text">
          <ul class="list-group" id="report-list">
            <li class="list-group-item">
              Cost: $${element.cost}
            </li>
            <li class="list-group-item">
                Revenue: $${element.revenue}
              </li>
          </ul>
        </div>
      </div>
    </div>`)
    });
    jQuery("#report-list").append(listItems.join(""));
  }).fail(err => {
    console.error("error getting the report");
  });
};

/**
 * Gets the list of appointments for the practitioner(s) selected
 * @param {practitionerId} practitionerId this is optional, if it is not defined, the function will try to find the global filter and if that is not defined, will bring all the appointments for all the practitioners
 */
function getAppointments(practitionerId, yearMonth) {

  let queryString = getQueryString(practitionerId, yearMonth ? yearMonth.toString() : "");

  // empty the list:
  jQuery("#appointment-list").html("");

  // since list of appointments has changed, the selected appintment should too
  jQuery("#appointment-details").html("");

  let listItems = [];
  jQuery.get("http://localhost:59489/api/appointments" + queryString).done(data => {
    data.forEach(element => {
      listItems.push(`<div class="card">
      <div class="card-body">
        <div class="card-title like-a-toolbar">
          <h5>${moment(element.date).format('DD/MM/YYYY')}</h5>
          <a href='#' onClick='getAppointmentDetails(${element.id}); return false;'>Details</a>
        </div>
        <div class="card-text">
          <ul class="list-group" id="report-list">
            <li class="list-group-item">
              Cost: $${element.cost}
            </li>
            <li class="list-group-item">
              Revenue: $${element.revenue}
            </li>
          </ul>
        </div>
      </div>
    </div>`)
    });
    jQuery("#appointment-list").append(listItems.join(""));
  }).fail(err => {
    console.error("error getting the list of appointments");
  });
};

// FIXME: appointment type doesn't say anything, there should be a table with those keys and human-readable names for them
function getAppointmentDetails(appointmentId) {
  jQuery.get("http://localhost:59489/api/appointments/" + appointmentId).done(element => {
    jQuery("#appointment-details").html(`<div class="card">
      <div class="card-body">
        <h5 class="card-title">${moment(element.date).format('DD/MM/YYYY')}</h5>
        <div class="card-text">
          <ul class="list-group" id="report-list">
            <li class="list-group-item">
              Cost: ${element.client_name}
            </li>
            <li class="list-group-item">
              Duration: ${element.duration} min.
            </li>
            <li class="list-group-item">
              Cost: $${element.cost}
            </li>
            <li class="list-group-item">
              Revenue: $${element.revenue}
            </li>
          </ul>
        </div>
      </div>
    </div>`);
  }).fail(err => {
    console.error("error getting the list of appointments");
  });
};

// get practitioners when the page is ready
$(document).ready(function() {
  getPractitioners();

  // testing
  getReport();
  getAppointments();
  getAppointmentDetails(139);
});