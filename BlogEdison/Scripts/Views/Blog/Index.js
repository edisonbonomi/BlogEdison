$(document).ready(function () {
    $('.excluir-post').on('click', function(e) {
        //se não confirmar
        if(!confirm('Deseja realmente excluir esse post?')) 
        {
            e.preventDefault();
        }

    })

});

