var ToDoFormOperator = function () {
    var self = this;
    self.createnRout = "/ToDo/CreateToDo";
    self.deleteRout = "/ToDo/DeleteToDo";
    self.changeStateRout = "/ToDo/IsCompleted";
    self.deleteAllRout = "/ToDo/DeleteAllCompleteToDo";
    
    self.SuccessRout = "/ToDo/List";

    self.Create = function () {
        var AddProfiles = {
            ToDo: $('#InputToDo').val(),
        };
        $.post(self.createnRout,AddProfiles)
       .done(function (data) {
           if (data.status=="success")
           {
               $.get(self.SuccessRout, function (data) {
                   $('#ToDoResult').html(data);
                   $('#InputToDo').val('');
               });
           }

       })
    }
    self.Delete = function (Id) {
        var AddProfiles = {
            Id: Id,
        };
        $.post(self.deleteRout, AddProfiles)
       .done(function (data) {
           if (data.status == "success") {
               $.get(self.SuccessRout, function (data) {
                   $('#ToDoResult').html(data);
                   $('#InputToDo').val('');
               });
           }

       })
    }
    self.DeleteAll = function () {
        $.post(self.deleteAllRout)
       .done(function (data) {
           if (data.status == "success") {
               $.get(self.SuccessRout, function (data) {
                   $('#ToDoResult').html(data);
                   $('#InputToDo').val('');
               });
           }

       })
    }
    self.ChangeStatus = function (Id, statusString) {
         var AddProfiles = {
            Id: Id,
            IsCompleted: statusString
        };
        $.post(self.changeStateRout, AddProfiles)
       .done(function (data) {
           if (data.status == "success") {
               $.get(self.SuccessRout, function (data) {
                   $('#ToDoResult').html(data);
                   $('#InputToDo').val('');
               });
           }

       })
    }
    self.GetList=function(status)
    {
        $.get(self.SuccessRout+"?status="+status, function (data) {
            $('#ToDoResult').html(data);
            $('#InputToDo').val('');
        });
    }
}

$(function () {
    ToDoFormoperator = new ToDoFormOperator();
    $("#InputToDo").keypress(function (e) {
        code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            ToDoFormoperator.Create();
        }
    });

});