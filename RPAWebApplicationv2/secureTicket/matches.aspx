<html>
<head>
	<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
	<script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
	<link rel="stylesheet" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
	
	<script>
		/* Formatting function for row details - modify as you need */
		function format ( d ) {
			// `d` is the original data object for the row
			return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">'+
				'<tr>'+
					'<td>Full name:</td>'+
					'<td>'+d.name+'</td>'+
				'</tr>'+
				'<tr>'+
					'<td>Extension number:</td>'+
					'<td>'+d.extn+'</td>'+
				'</tr>'+
				'<tr>'+
					'<td>Extra info:</td>'+
					'<td>And any further details here (images etc)...</td>'+
				'</tr>'+
			'</table>';
		}
		 
		$(document).ready(function() {
            var table = $('#scriptTable').DataTable({
                "paging": false,
                "ordering": false,
                "info": false,
                "searching": false,
                "ajax": {
                    "url": "/secureTicket/GetOptions?ticketID=12345",
                    "dataSrc": function (json)
                    {
                        result = JSON.parse(json);
                        return result.data;
                    }
                },                
				"columns": [
					{
						"className":      'details-control',
						"orderable":      false,
						"data":           null,
						"defaultContent": ''
					},
					{ "data": "name" },
					{ "data": "position" },
					{ "data": "office" },
					{ "data": "salary" }
				],
				"order": [[1, 'asc']]
			} );
			 
			// Add event listener for opening and closing details
			$('#scriptTable tbody').on('click', 'td.details-control', function () {
				var tr = $(this).closest('tr');
				var row = table.row( tr );
		 
				if ( row.child.isShown() ) {
					// This row is already open - close it
					row.child.hide();
					tr.removeClass('shown');
				}
				else {
					// Open this row
					row.child( format(row.data()) ).show();
					tr.addClass('shown');
				}
			} );
		} );
	</script>
	<style>
		td.details-control {
			background: url('../resources/details_open.png') no-repeat center center;
			cursor: pointer;
		}
		tr.shown td.details-control {
			background: url('../resources/details_close.png') no-repeat center center;
		}
	</style>
</head>
<body>
<table id="scriptTable" class="display" cellspacing="0" width="100%">
	<thead>
		<tr>
			<th></th>
			<th>Name</th>
			<th>Position</th>
			<th>Office</th>
			<th>Salary</th>
		</tr>
	</thead>
	<tfoot>
		<tr>
			<th></th>
			<th>Name</th>
			<th>Position</th>
			<th>Office</th>
			<th>Salary</th>
		</tr>
	</tfoot>
    </table>
</body>
</html>