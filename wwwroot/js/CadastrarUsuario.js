$(document).ready(function () {
    $("#email").val("");
    $("#senha").val("");

});

$("#senhaRepetida").change(function () {
    
    var primeiraSenha = $("#senha").val();
    var senhaRepetida = $("#senhaRepetida").val();
    if (primeiraSenha == senhaRepetida) {
        
    }
    else
    {
        
        Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Senhas Não Correspondem...",
            
        })
        $("#senhaRepetida").val("");
    }
});

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

$("#email").change(function () {
   
    var email = $("#email").val();
    
    if (isEmail(email)) {
        
        $(function () {
            $.ajax({
                contentType: "application/json",
                type: "GET",
                url: "/Usuario/VerificarEmail",
                data: { "emailUsuario": email },
                success:
                    function (data) {
                        
                        if (data == true) {
                            Swal.fire({
                                icon: "error",
                                title: "Oops...",
                                text: "Email já Existe...",

                            })
                            $("#email").val("");
                        }
                        else
                        {
                                
                        }


                    }
            });
        });

    }

    
});

