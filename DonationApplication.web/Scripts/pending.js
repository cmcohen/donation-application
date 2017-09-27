$(function () {
    fillTable();

    function fillTable() {
        $.get('/admin/GetPendingApplications', function (applications) {
            $("table tr:gt(0)").remove();
            applications.forEach(app => {
                var row = `<tr>
                    category = a.Category.Name,
                email = a.User.Email,
                firstName = a.User.FirstName,
                lastName = a.User.LastName,
                donationAmount = a.Amount,
                status = a.Status
                            <td>${app.category}</td>
                            <td><a href="">${app.email}</a></td>
                            <td>${app.firstName}</td>
                            <td>${app.lastName}</td>
                            <td>${app.donationAmount}</td>
                            <td>
                                <button class ='btn btn-success approve'data-app-id="${app.id}">Approve</button>
                                <button class ='btn btn-danger reject'data-app-id="${app.id}">Reject</button>
                            </td>
                           </tr>`;
                $("table").append(row);
            });
        });
    }

    $(".table").on('click', '.approve', function () {
        var appId = $(this).data('app-id');
        var isApproved = true;
        $.post('/admin/updateStatus', { applicationId: appId, status: isApproved }, function () {
            fillTable();
        });
    });


    $(".table").on('click', '.reject', function () {
        var appId = $(this).data('app-id');
        var isApproved = false;
        $.post('/admin/updateStatus', { applicationId: appId, status: isApproved }, function () {
            fillTable();
        });
    });
});